﻿using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace Mario_vNext.Core.SystemExt
{
    class Keyboard
    {
        [DllImport("user32.dll")]
        public static extern ushort GetKeyState(short nVirtKey);

        public const ushort keyDownBit = 0x80;

        public bool IsKeyPressed(ConsoleKey key)
        {
            return ((GetKeyState((short)key) & keyDownBit) == keyDownBit);
        }
        
        public Keyboard(CancellationTokenSource reference)
        {
            tokenReference = reference;
            Task.Factory.StartNew(() => CheckForKeys());
        }

        CancellationTokenSource tokenReference;

        public Action onEnterKey { get; set; }
        public Action onWKey { get; set; }
        public Action onSKey { get; set; }
        public Action onAKey { get; set; }
        public Action onDKey { get; set; }
        public Action onQKey { get; set; }
        public Action onEKey { get; set; }
        public Action onEndKey { get; set; }
        public Action onDeleteKey { get; set; }
        public Action onEscKey { get; set; }
        public Action onSpaceKey { get; set; }
        public Action onFKey { get; set; }
        public Action onBackSpaceKey { get; set; }
        public Action onInsertKey { get; set; }
        public Action onZKey { get; set; }
        public Action onPageUpKey { get; set; }
        public Action onPageDownKey { get; set; }

        private void CheckForKeys()
        {
            while (true)
            {
                if (tokenReference.IsCancellationRequested)
                {
                    return;
                }

                Thread.Sleep(50);

                if (this.IsKeyPressed(ConsoleKey.W))
                {
                    onWKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.S))
                {
                    onSKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.A))
                {
                    onAKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.D))
                {
                    onDKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.Q))
                {
                    onQKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.E))
                {
                    onEKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.F))
                {
                    onFKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.Spacebar))
                {
                    onSpaceKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.Backspace))
                {
                    onBackSpaceKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.Enter))
                {
                    onEnterKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.Insert))
                {
                    onInsertKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.Delete))
                {
                    onDeleteKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.Escape))
                {
                    onEscKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.Z))
                {
                    onZKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.PageUp))
                {
                    onPageUpKey?.Invoke();
                }

                if (this.IsKeyPressed(ConsoleKey.Z))
                {
                    onPageDownKey?.Invoke();
                }
            }
        }
    }
}