using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int moveID = 0;
        static String[] elements = { "Normal", "Air", "Fire", "Water", "Earth",
            "Lightning", "Steel", "Energy", "Blood", "Nature", "Lunar", "Explosive"};
        static String[] modifiers = { "", "SwitchMNST", "Reflect", "Protect", "PreviousMove" };
        static String[] statusEffects = { "", "Poison", "Hypnotize", "Bleed", "Damage", "Statuses", "Prevent", "Use"};
        static String[] statModifiers = { "", "Power", "Speed", "Defense", "MaxHealthPercent", "CurrentHealthPercent"};
        public MainWindow()
        {
            InitializeComponent();
            for(int i = 0; i < elements.Length; i++)
            {
                comboElement.Items.Add(elements[i]);
            }
            for (int i = 0; i < statusEffects.Length; i++)
            {
                comboStatusEffectSelf.Items.Add(statusEffects[i]);
                comboStatusEffectOpponent.Items.Add(statusEffects[i]);
            }
            for (int i = 0; i < modifiers.Length; i++)
            {
                comboModifierSelf.Items.Add(modifiers[i]);
                comboModifierOpponent.Items.Add(modifiers[i]);
            }
            for (int i = 0; i < statModifiers.Length; i++)
            {
                comboStatModifierSelf.Items.Add(statModifiers[i]);
                comboStatModifierOpponent.Items.Add(statModifiers[i]);
            }
        }

        //MNST CREATE BUTTON
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(txtNewMnst.Text.Length > 0)
            {
                if(!listMNST.Items.Contains(txtNewMnst.Text))
                {
                    listMNST.Items.Add(txtNewMnst.Text);
                    listMNST.UpdateLayout();
                    txtNewMnst.Clear();
                }
            }
        }

        private void btnRemoveMNST_Click(object sender, RoutedEventArgs e)
        {
            if (listMNST.SelectedIndex >= 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to remove " + listMNST.Items.GetItemAt(listMNST.SelectedIndex).ToString() + "?",
              "Remove MNST?",
              MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    listMNST.Items.RemoveAt(listMNST.SelectedIndex);
                }
            }
        }

        private void btnAddFront_Click(object sender, RoutedEventArgs e)
        {
            frontImage.Source = getImageFromFileBrowse();
        }

        private void btnAddBack_Click(object sender, RoutedEventArgs e)
        {
            backImage.Source = getImageFromFileBrowse();
        }

        private BitmapImage getImageFromFileBrowse()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "png|*.png";
            Nullable<bool> result = fd.ShowDialog();
            if (result == true)
            {
                BitmapImage img = new BitmapImage();
                return new BitmapImage(new Uri(fd.FileName));
            }
            return null;
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (listMNST.Items.Count > 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Do you want to save changes to your current work?",
              "Save Changes?",
              MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    if (ExportChanges())
                    {
                        ImportFile();
                    }
                }
                else
                {
                    ImportFile();
                }
            }
            else
            {
                ImportFile();
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            ExportChanges();
        }

        private bool ExportChanges()
        {
            SaveFileDialog fd = new SaveFileDialog();
            fd.Filter = "json|*.json";
            Nullable<bool> result = fd.ShowDialog();
            return result == null ? false : true;
        }
        private bool ImportFile()
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "json|*.json";
            Nullable<bool> result = fd.ShowDialog();
            return result == null ? false : true;
        }

        private void btnAddMonsterMove_Click(object sender, RoutedEventArgs e)
        {
            moveID++;
            Grid grid = new Grid();

            ComboBox box = new ComboBox();
            box.Name = "box" + moveID;
            box.Items.Add("Test");
            box.Items.Add("Bad Move");
            box.Width = 150;
            box.HorizontalAlignment = HorizontalAlignment.Left;
            grid.Children.Add(box);

            TextBox move = new TextBox();
            move.Name = "move" + moveID;
            move.Text = moveID.ToString();
            move.Width = 30;
            move.HorizontalAlignment = HorizontalAlignment.Right;
            grid.Children.Add(move);

            movePanel.Children.Add(grid);
        }

        // MOVE TAB
        private void btnAddMove_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewMove.Text.Length > 0)
            {
                if (!listNewMoves.Items.Contains(txtNewMove.Text))
                {
                    listNewMoves.Items.Add(txtNewMove.Text);
                    listNewMoves.UpdateLayout();
                    txtNewMove.Clear();
                }
            }
        }

        private void btnAddMoveIcon_Click(object sender, RoutedEventArgs e)
        {
            imageMoveIcon.Source = getImageFromFileBrowse();
        }

        private void btnImportMoves_Click(object sender, RoutedEventArgs e)
        {
            if (listNewMoves.Items.Count > 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Do you want to save changes to your current work?",
              "Save Changes?",
              MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    if (ExportChanges())
                    {
                        ImportFile();
                    }
                }
                else
                {
                    ImportFile();
                }
            }
            else
            {
                ImportFile();
            }
        }

        private void btnExportMoves_Click(object sender, RoutedEventArgs e)
        {
            ExportChanges();
        }

        // ZONES
        private void btnRemoveZone_Click(object sender, RoutedEventArgs e)
        {
            if (listZones.SelectedIndex >= 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to remove " + listZones.Items.GetItemAt(listZones.SelectedIndex).ToString() + "?",
              "Remove Zone?",
              MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    listZones.Items.RemoveAt(listZones.SelectedIndex);
                }
            }
        }

        private void btnAddZoneImageSmall_Click(object sender, RoutedEventArgs e)
        {
            imageZoneSmall.Source = getImageFromFileBrowse();
        }

        private void btnAddZoneImageLarge_Click(object sender, RoutedEventArgs e)
        {
            imageZoneLarge.Source = getImageFromFileBrowse();
        }

        private void btnAddZone_Click(object sender, RoutedEventArgs e)
        {
            if (txtAddZone.Text.Length > 0)
            {
                if (!listZones.Items.Contains(txtAddZone.Text))
                {
                    listZones.Items.Add(txtAddZone.Text);
                    listZones.UpdateLayout();
                    txtAddZone.Clear();
                }
            }
            
        }

        private void btnExportZones_Click(object sender, RoutedEventArgs e)
        {
            ExportChanges();
        }

        private void btnImportZones_Click(object sender, RoutedEventArgs e)
        {
            if (listZones.Items.Count > 0)
            {
                MessageBoxResult dialogResult = MessageBox.Show("Do you want to save changes to your current work?",
              "Save Changes?",
              MessageBoxButton.YesNo);
                if (dialogResult == MessageBoxResult.Yes)
                {
                    if (ExportChanges())
                    {
                        ImportFile();
                    }
                }
                else
                {
                    ImportFile();
                }
            }
            else
            {
                ImportFile();
            }
        }
    }
}
