﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AuroraPatch
{
    public partial class Form1 : Form
    {
        private readonly Loader Loader;

        internal Form1(Loader loader) : base()
        {
            Loader = loader;
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            var patches = Loader.FindPatches().ToList();
            Loader.StartAurora(patches);
        }
    }
}
