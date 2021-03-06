﻿using System.IO;
using System.Threading.Tasks;

namespace StandardStorage
{
    /// <summary>
    /// Provides extension methods for the <see cref="IFile"/> class
    /// </summary>
    public static class FileExtensions
    {
        /// <summary>
        /// Reads the contents of a file as a string
        /// </summary>
        /// <param name="file">The file to read </param>
        /// <returns>The contents of the file</returns>
        public static async Task<string> ReadAllTextAsync(this IFile file)
        {
            using (Stream stream = await file.OpenAsync(FileAccess.Read).ConfigureAwait(false))
            using (StreamReader sr = new StreamReader(stream))
                return await sr.ReadToEndAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Writes text to a file, overwriting any existing data
        /// </summary>
        /// <param name="file">The file to write to</param>
        /// <param name="contents">The content to write to the file</param>
        /// <returns>A task which completes when the write operation finishes</returns>
        public static async Task WriteAllTextAsync(this IFile file, string contents)
        {
            using (Stream stream = await file.OpenAsync(FileAccess.ReadWrite).ConfigureAwait(false))
            {
                stream.SetLength(0);
                using (StreamWriter sw = new StreamWriter(stream))
                    await sw.WriteAsync(contents).ConfigureAwait(false);
            }
        }
    }
}