using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace WlToolsLib.Extension
{
    public static class AsyncAwaitExtension
    {
        public static StringAwait GetAwaiter(this string self)
        {
            Console.WriteLine(self);
            return new StringAwait();
        }

        public static IntAwait GetAwaiter(this int self)
        {
            Console.WriteLine(self);
            return new IntAwait();
        }

        public static LongAwait GetAwaiter(this long self)
        {
            Console.WriteLine(self);
            return new LongAwait();
        }

        public static DecimalAwait GetAwaiter(this decimal self)
        {
            Console.WriteLine(self);
            return new DecimalAwait();
        }

        public static FloatAwait GetAwaiter(this float self)
        {
            Console.WriteLine(self);
            return new FloatAwait();
        }
    }

    public class StringAwait : INotifyCompletion
    {
        public bool IsCompleted { get; }

        public void GetResult()
        {
        }

        /// <inheritdoc />
        public void OnCompleted(Action continuation)
        {
            continuation();
        }
    }

    public class IntAwait : INotifyCompletion
    {
        public bool IsCompleted { get; }

        public void GetResult()
        {
        }

        /// <inheritdoc />
        public void OnCompleted(Action continuation)
        {
            continuation();
        }
    }

    public class LongAwait : INotifyCompletion
    {
        public bool IsCompleted { get; }

        public void GetResult()
        {
        }

        /// <inheritdoc />
        public void OnCompleted(Action continuation)
        {
            continuation();
        }
    }

    public class DecimalAwait : INotifyCompletion
    {
        public bool IsCompleted { get; }

        public void GetResult()
        {
        }

        /// <inheritdoc />
        public void OnCompleted(Action continuation)
        {
            continuation();
        }
    }

    public class FloatAwait : INotifyCompletion
    {
        public bool IsCompleted { get; }

        public void GetResult()
        {
        }

        /// <inheritdoc />
        public void OnCompleted(Action continuation)
        {
            continuation();
        }
    }
}
