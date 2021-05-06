﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AuroraPatch
{
    public partial class Form1 : Form
    {
        private readonly Loader Loader;
        private readonly List<Patch> Patches;

        internal Form1(Loader loader) : base()
        {
            InitializeComponent();

            Loader = loader;
            Patches = Loader.FindPatches();
            Loader.SortPatches(Patches);

            UpdateList();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            foreach (var missing in Loader.GetUnmetDependencies(Patches))
            {
                MessageBox.Show($"Patch {missing.Key.Name} missing dependency {missing.Value}");
                
                return;
            }

            Loader.StartAurora(Patches);
        }

        private void UpdateList()
        {
            ListPatches.Items.Clear();
            ListPatches.Items.AddRange(Patches.Select(p => p.Name).ToArray());

            if (ListPatches.Items.Count > 0)
            {
                ListPatches.SelectedIndex = 0;
            }

            UpdateDescription();
        }

        private void UpdateDescription()
        {
            var index = ListPatches.SelectedIndex;
            if (index >= 0)
            {
                var patch = Patches.Single(p => p.Name == (string)ListPatches.Items[index]);
                LabelDescription.Text = $"Description: {patch.Description}";
            }
            else
            {
                LabelDescription.Text = "Description:";
            }     
        }

        private void ButtonChangeSettings_Click(object sender, EventArgs e)
        {
            var index = ListPatches.SelectedIndex;
            if (index >= 0)
            {
                var patch = Patches.Single(p => p.Name == (string)ListPatches.Items[index]);
                patch.ChangeSettingsInternal();
            }
        }

        private void ListPatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDescription();
        }
    }
}