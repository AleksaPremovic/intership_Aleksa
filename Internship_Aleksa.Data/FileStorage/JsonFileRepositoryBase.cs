using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace Internship_Aleksa.Data.FileStorage;

public abstract class JsonFileRepositoryBase<T>
    {
        private static readonly ConcurrentDictionary<string, SemaphoreSlim> _locks = new();
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions;

        protected JsonFileRepositoryBase(FileStorageOptions options, string fileName)
        {
            var basePath = options.BasePath ?? "App_Data";
            if (!Path.IsPathRooted(basePath))
                basePath = Path.Combine(AppContext.BaseDirectory, basePath);

            Directory.CreateDirectory(basePath);
            _filePath = Path.Combine(basePath, fileName);

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = options.PrettyPrint,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        protected abstract int GetId(T entity);
        protected abstract void SetId(T entity, int id);

        protected virtual async Task<List<T>> ReadAllAsync()
        {
            var gate = _locks.GetOrAdd(_filePath, _ => new SemaphoreSlim(1,1));
            await gate.WaitAsync();
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<T>();
                }

                using var stream = File.OpenRead(_filePath);
                var data = await JsonSerializer.DeserializeAsync<List<T>>(stream, _jsonOptions);
                return data ?? new List<T>();
            }
            finally
            {
                gate.Release();
            }
        }

        protected virtual async Task WriteAllAsync(List<T> items)
        {
            var gate = _locks.GetOrAdd(_filePath, _ => new SemaphoreSlim(1,1));
            await gate.WaitAsync();
            try
            {
                using var stream = File.Create(_filePath);
                await JsonSerializer.SerializeAsync(stream, items, _jsonOptions);
            }
            finally
            {
                gate.Release();
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await ReadAllAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            var all = await ReadAllAsync();
            return all.FirstOrDefault(e => GetId(e) == id);
        }

        public virtual async Task AddAsync(T entity)
        {
            var all = await ReadAllAsync();
            var nextId = all.Count == 0 ? 1 : all.Max(e => GetId(e)) + 1;
            SetId(entity, nextId);
            all.Add(entity);
            await WriteAllAsync(all);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            var all = await ReadAllAsync();
            var idx = all.FindIndex(e => GetId(e) == GetId(entity));
            if (idx < 0) throw new KeyNotFoundException($"Entity with id {GetId(entity)} not found.");
            all[idx] = entity;
            await WriteAllAsync(all);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var all = await ReadAllAsync();
            var removed = all.RemoveAll(e => GetId(e) == id);
            if (removed > 0)
                await WriteAllAsync(all);
        }
    }