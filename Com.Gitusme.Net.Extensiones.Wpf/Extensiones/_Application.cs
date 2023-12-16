/*********************************************************
 * Copyright (c) 2019-2023 gitusme, All rights reserved.
 *********************************************************/

using System;
using System.Threading;
using System.Windows;

namespace Com.Gitusme.Net.Extensiones.Wpf
{
    public static class _Application
    {
        public static void StartSplashScreen(
            this Application @this, string splashScreen, Action doWork, Action onClosed)
        {
            StartSplashScreen(
                @this, splashScreen, doWork, onClosed, null);
        }

        public static void StartSplashScreen(
            this Application @this, string splashScreen, Action doWork, Action onClosed, Action<Exception>? onError)
        {
            try
            {
                StartSplashScreen(@this, new SplashScreen(splashScreen), doWork, onClosed, onError);
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        }

        public static void StartSplashScreen(
            this Application @this, SplashScreen splashScreen, Action doWork, Action onClosed)
        {
            StartSplashScreen(
                @this, splashScreen, doWork, onClosed, null);
        }

        public static void StartSplashScreen(
            this Application @this, SplashScreen splashScreen, Action doWork, Action onClosed, Action<Exception>? onError)
        {
            Action show = () => splashScreen?.Show(false);
            Action close = () => splashScreen?.Close(new TimeSpan(0));

            Run(@this, show, close, doWork, onClosed, onError);
        }

        public static void StartSplashScreen(
            this Application @this, Window splashScreen, Action doWork, Action onClosed)
        {
            StartSplashScreen(
                @this, splashScreen, doWork, onClosed, null);
        }

        public static void StartSplashScreen(
            this Application @this, Window splashScreen, Action doWork, Action onClosed, Action<Exception>? onError)
        {
            Action show = () => splashScreen?.Show();
            Action close = () => splashScreen?.Close();

            Run(@this, show, close, doWork, onClosed, onError);
        }

        private static void Run(
            this Application @this, Action show, Action close, Action doWork, Action onClosed, Action<Exception>? onError)
        {
            try
            {
                show?.Invoke();

                Thread thread = new Thread(() =>
                {
                    doWork?.Invoke();
                    @this?.Dispatcher.InvokeAsync(new Action(() =>
                    {
                        close?.Invoke();
                        onClosed?.Invoke();
                    }));
                });
                thread.Start();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
            }
        }
    }
}
