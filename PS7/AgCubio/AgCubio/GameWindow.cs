﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Network_Controller;

namespace AgCubio
{
    public partial class GameWindow : Form
    {
        private readonly World _world;

        private readonly NetworkManager _networkManager;

        public GameWindow()
        {
            InitializeComponent();
            _world = new World();
            _world.AddCube(new Cube
            {
                Color = Color.Aqua,
                Coord = new Point(34, 67),
                Mass = 90,
                IsFood = false,
                Name = "hi",
                Uid = 0
            });

            _networkManager = NetworkManager.Create();
        }
        
        private void GameWindow_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            
            foreach (KeyValuePair<int, Cube> cube in _world.Cubes)
            {
                DrawCube(g, cube.Value);
            }
        }

        private void GameWindow_MouseMove(object sender, MouseEventArgs e)
        {

        }


        #region Drawing

        /// <summary>
        /// Draws the specified cube
        /// </summary>
        /// <param name="g">Graphics to draw the cube on</param>
        /// <param name="cube">The cube to draw</param>
        private void DrawCube(Graphics g, Cube cube)
        {
            Brush b = new SolidBrush(cube.Color);
            g.FillRectangle(b, cube.Left, cube.Top,
                cube.Width, cube.Width);
        }

        #endregion


        #region Utils

        /// <summary>
        /// Used to do operations on the underlying business logic thread.
        /// </summary>
        /// <param name="work"></param>
        private void DoBackgroundWork(Action<DoWorkEventArgs> work)
        {
            var b = new BackgroundWorker();
            b.DoWork += (sender, e) => work(e);
            b.RunWorkerAsync();
        }

        private static readonly object ForegroundLock = new object();

        /// <summary>
        /// Used to do operations on the GUI thread.
        /// </summary>
        /// <param name="work"></param>
        private void DoForegroundWork(Action work)
        {
            try
            {
                if (InvokeRequired)
                {
                    BeginInvoke(work);
                }
                else
                {
                    lock (ForegroundLock)
                    {
                        work();
                    }
                }
            }
            catch
            {
            }

        }

        #endregion

        private void GameWindow_Load(object sender, EventArgs e)
        {
            (new ConnectForm()).ShowDialog(this);

            CheckConnected();
        }

        private void CheckConnected()
        {
            if (_networkManager.Client.Connected) return;

            var result = MessageBox.Show(@"You must connect to a server to play AgCubio", @"Must connect to server",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            if (result == DialogResult.OK)
            {
                (new ConnectForm()).ShowDialog(this);
                CheckConnected();
            }

            if (result == DialogResult.Cancel)
                Close();
        }
    }
}
