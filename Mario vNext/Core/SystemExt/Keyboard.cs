using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Timers;

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

        public Keyboard(int numericalInterval, int specialInterval, int alphaInterval)
        {
            checkerNumber.AutoReset = true;
            checkerNumber.Interval = numericalInterval;

            checkerAlpha.AutoReset = true;
            checkerAlpha.Interval = alphaInterval;

            checkerSpecial.AutoReset = true;
            checkerSpecial.Interval = specialInterval;

            checkerAlpha.Elapsed += onAKeyEvent;
            checkerAlpha.Elapsed += onBKeyEvent;
            checkerAlpha.Elapsed += onCKeyEvent;
            checkerAlpha.Elapsed += onDKeyEvent;
            checkerAlpha.Elapsed += onEKeyEvent;
            checkerAlpha.Elapsed += onFKeyEvent;
            checkerAlpha.Elapsed += onGKeyEvent;
            checkerAlpha.Elapsed += onHKeyEvent;
            checkerAlpha.Elapsed += onIKeyEvent;
            checkerAlpha.Elapsed += onJKeyEvent;
            checkerAlpha.Elapsed += onKKeyEvent;
            checkerAlpha.Elapsed += onLKeyEvent;
            checkerAlpha.Elapsed += onMKeyEvent;
            checkerAlpha.Elapsed += onNKeyEvent;
            checkerAlpha.Elapsed += onOKeyEvent;
            checkerAlpha.Elapsed += onPKeyEvent;
            checkerAlpha.Elapsed += onQKeyEvent;
            checkerAlpha.Elapsed += onRKeyEvent;
            checkerAlpha.Elapsed += onSKeyEvent;
            checkerAlpha.Elapsed += onTKeyEvent;
            checkerAlpha.Elapsed += onUKeyEvent;
            checkerAlpha.Elapsed += onVKeyEvent;
            checkerAlpha.Elapsed += onWKeyEvent;
            checkerAlpha.Elapsed += onXKeyEvent;
            checkerAlpha.Elapsed += onYKeyEvent;
            checkerAlpha.Elapsed += onZKeyEvent;
            checkerNumber.Elapsed += onN0KeyEvent;
            checkerNumber.Elapsed += onN1KeyEvent;
            checkerNumber.Elapsed += onN2KeyEvent;
            checkerNumber.Elapsed += onN3KeyEvent;
            checkerNumber.Elapsed += onN4KeyEvent;
            checkerNumber.Elapsed += onN5KeyEvent;
            checkerNumber.Elapsed += onN6KeyEvent;
            checkerNumber.Elapsed += onN7KeyEvent;
            checkerNumber.Elapsed += onN8KeyEvent;
            checkerNumber.Elapsed += onN9KeyEvent;
            checkerSpecial.Elapsed += onEnterKeyEvent;
            checkerSpecial.Elapsed += onEndKeyEvent;
            checkerSpecial.Elapsed += onDeleteKeyEvent;
            checkerSpecial.Elapsed += onEscapeKeyEvent;
            checkerSpecial.Elapsed += onSpaceKeyEvent;
            checkerSpecial.Elapsed += onBackSpaceKeyEvent;
            checkerSpecial.Elapsed += onInsertKeyEvent;
            checkerSpecial.Elapsed += onPageUpKeyEvent;
            checkerSpecial.Elapsed += onPageDownKeyEvent;
            checkerSpecial.Elapsed += onUpArrowKeyEvent;
            checkerSpecial.Elapsed += onDownArrowKeyEvent;
            checkerSpecial.Elapsed += onLeftArrowKeyEvent;
            checkerSpecial.Elapsed += onRightArrowKeyEvent;
        }

        
        public void Start()
        {
            checkerNumber.Start();
            checkerSpecial.Start();
            checkerAlpha.Start();
        }

        public void Abort()
        {
            checkerNumber.Stop();
            checkerSpecial.Stop();
            checkerAlpha.Stop();
        }

        Timer checkerNumber = new Timer();
        Timer checkerAlpha = new Timer();
        Timer checkerSpecial = new Timer();


        //
        //  Alpha keys
        //
        public Action onAKey { get; set; }
        public Action onBKey { get; set; }
        public Action onCKey { get; set; }
        public Action onDKey { get; set; }
        public Action onEKey { get; set; }
        public Action onFKey { get; set; }
        public Action onGKey { get; set; }
        public Action onHKey { get; set; }
        public Action onIKey { get; set; }
        public Action onJKey { get; set; }
        public Action onKKey { get; set; }
        public Action onLKey { get; set; }
        public Action onMKey { get; set; }
        public Action onNKey { get; set; }
        public Action onOKey { get; set; }
        public Action onPKey { get; set; }
        public Action onQKey { get; set; }
        public Action onRKey { get; set; }
        public Action onSKey { get; set; }
        public Action onTKey { get; set; }
        public Action onUKey { get; set; }
        public Action onVKey { get; set; }
        public Action onWKey { get; set; }
        public Action onXKey { get; set; }
        public Action onYKey { get; set; }
        public Action onZKey { get; set; }

        //
        //  Numerical keys
        //

        public Action onN0ArrowKey { get; set; }
        public Action onN1ArrowKey { get; set; }
        public Action onN2ArrowKey { get; set; }
        public Action onN3ArrowKey { get; set; }
        public Action onN4ArrowKey { get; set; }
        public Action onN5ArrowKey { get; set; }
        public Action onN6ArrowKey { get; set; }
        public Action onN7ArrowKey { get; set; }
        public Action onN8ArrowKey { get; set; }
        public Action onN9ArrowKey { get; set; }

        //
        //  Special keys
        //

        public Action onEnterKey { get; set; }
        public Action onEndKey { get; set; }
        public Action onDeleteKey { get; set; }
        public Action onEscKey { get; set; }
        public Action onSpaceKey { get; set; }
        public Action onBackSpaceKey { get; set; }
        public Action onInsertKey { get; set; }
        public Action onPageUpKey { get; set; }
        public Action onPageDownKey { get; set; }
        public Action onUpArrowKey { get; set; }
        public Action onDownArrowKey { get; set; }
        public Action onLeftArrowKey { get; set; }
        public Action onRightArrowKey { get; set; }
        
        public void onAlphaNumericalKeys(Action ac)
        {
            onNumericalKeys(ac);
            OnAlphaKeys(ac);
        }

        public void onNumericalKeys(Action ac)
        {
            onN0ArrowKey = ac;
            onN1ArrowKey = ac;
            onN2ArrowKey = ac;
            onN3ArrowKey = ac;
            onN4ArrowKey = ac;
            onN5ArrowKey = ac;
            onN6ArrowKey = ac;
            onN7ArrowKey = ac;
            onN8ArrowKey = ac;
            onN9ArrowKey = ac;
        }

        public void OnAlphaKeys(Action ac)
        {
            onAKey = ac;
            onBKey = ac;
            onCKey = ac;
            onDKey = ac;
            onEKey = ac;
            onFKey = ac;
            onGKey = ac;
            onHKey = ac;
            onIKey = ac;
            onJKey = ac;
            onKKey = ac;
            onLKey = ac;
            onMKey = ac;
            onNKey = ac;
            onOKey = ac;
            onPKey = ac;
            onQKey = ac;
            onRKey = ac;
            onSKey = ac;
            onTKey = ac;
            onUKey = ac;
            onVKey = ac;
            onWKey = ac;
            onXKey = ac;
            onYKey = ac;
            onZKey = ac;
        }

        private void onAKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.A))
            {
                onAKey?.Invoke();
            }
        }

        private void onBKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.B))
            {
                onBKey?.Invoke();
            }
        }

        private void onCKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.C))
            {
                onCKey?.Invoke();
            }
        }

        private void onDKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.D))
            {
                onDKey?.Invoke();
            }
        }

        private void onEKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.E))
            {
                onEKey?.Invoke();
            }
        }

        private void onFKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.F))
            {
                onFKey?.Invoke();
            }
        }

        private void onGKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.G))
            {
                onGKey?.Invoke();
            }
        }

        private void onHKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.H))
            {
                onHKey?.Invoke();
            }
        }

        private void onIKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.I))
            {
                onIKey?.Invoke();
            }
        }

        private void onJKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.J))
            {
                onJKey?.Invoke();
            }
        }

        private void onKKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.K))
            {
                onKKey?.Invoke();
            }
        }

        private void onLKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.L))
            {
                onLKey?.Invoke();
            }
        }

        private void onMKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.M))
            {
                onMKey?.Invoke();
            }
        }

        private void onNKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.N))
            {
                onNKey?.Invoke();
            }
        }

        private void onOKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.O))
            {
                onOKey?.Invoke();
            }
        }

        private void onPKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.P))
            {
                onPKey?.Invoke();
            }
        }

        private void onQKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.Q))
            {
                onQKey?.Invoke();
            }
        }

        private void onRKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.R))
            {
                onRKey?.Invoke();
            }
        }

        private void onSKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.S))
            {
                onSKey?.Invoke();
            }
        }

        private void onTKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.T))
            {
                onTKey?.Invoke();
            }
        }

        private void onUKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.U))
            {
                onUKey?.Invoke();
            }
        }

        private void onVKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.V))
            {
                onVKey?.Invoke();
            }
        }

        private void onWKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.W))
            {
                onWKey?.Invoke();
            }
        }

        private void onXKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.X))
            {
                onXKey?.Invoke();
            }
        }

        private void onYKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.Y))
            {
                onYKey?.Invoke();
            }
        }

        private void onZKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.Z))
            {
                onZKey?.Invoke();
            }
        }

        private void onN0KeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.NumPad0))
            {
                onN0ArrowKey?.Invoke();
            }
        }

        private void onN1KeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.NumPad1))
            {
                onN1ArrowKey?.Invoke();
            }
        }

        private void onN2KeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.NumPad2))
            {
                onN2ArrowKey?.Invoke();
            }
        }

        private void onN3KeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.NumPad3))
            {
                onN3ArrowKey?.Invoke();
            }
        }

        private void onN4KeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.NumPad4))
            {
                onN4ArrowKey?.Invoke();
            }
        }

        private void onN5KeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.NumPad5))
            {
                onN5ArrowKey?.Invoke();
            }
        }

        private void onN6KeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.NumPad6))
            {
                onN6ArrowKey?.Invoke();
            }
        }

        private void onN7KeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.NumPad7))
            {
                onN7ArrowKey?.Invoke();
            }
        }

        private void onN8KeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.NumPad8))
            {
                onN8ArrowKey?.Invoke();
            }
        }

        private void onN9KeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.NumPad9))
            {
                onN9ArrowKey?.Invoke();
            }
        }

        //
        //  Other
        //

        private void onEnterKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.Enter))
            {
                onEnterKey?.Invoke();
            }
        }

        private void onEndKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.End))
            {
                onEndKey?.Invoke();
            }
        }

        private void onDeleteKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.Delete))
            {
                onDeleteKey?.Invoke();
            }
        }

        private void onEscapeKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.Escape))
            {
                onEscKey?.Invoke();
            }
        }

        private void onSpaceKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.Spacebar))
            {
                onSpaceKey?.Invoke();
            }
        }

        private void onBackSpaceKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.Backspace))
            {
                onBackSpaceKey?.Invoke();
            }
        }
        
        private void onInsertKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.Insert))
            {
                onInsertKey?.Invoke();
            }
        }
        
        private void onPageUpKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.PageUp))
            {
                onPageUpKey?.Invoke();
            }
        }

        private void onPageDownKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.PageDown))
            {
                onPageDownKey?.Invoke();
            }
        }

        private void onUpArrowKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.UpArrow))
            {
                onUpArrowKey?.Invoke();
            }
        }

        private void onDownArrowKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.DownArrow))
            {
                onDownArrowKey?.Invoke();
            }
        }

        private void onLeftArrowKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.LeftArrow))
            {
                onLeftArrowKey?.Invoke();
            }
        }

        private void onRightArrowKeyEvent(object sender, ElapsedEventArgs e)
        {
            if (this.IsKeyPressed(ConsoleKey.RightArrow))
            {
                onRightArrowKey?.Invoke();
            }
        }
    }
}
