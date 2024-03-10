using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotesApp
{
    public partial class Form1 : Form
    {
        private List<string> notes;

        public Form1()
        {
            InitializeComponent();
            notes = new List<string>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Add a new note
            string noteText = noteTextBox.Text;
            if (!string.IsNullOrWhiteSpace(noteText))
            {
                notes.Add(noteText);
                notesListBox.Items.Add($"Note {notes.Count}");
                noteTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Enter the note text.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Save notes to a text file
            using (StreamWriter writer = new StreamWriter("notes.txt"))
            {
                foreach (string note in notes)
                {
                    writer.WriteLine(note);
                }
            }
            MessageBox.Show("Notes saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Load notes from a text file
            try
            {
                notes.Clear();
                notesListBox.Items.Clear();

                using (StreamReader reader = new StreamReader("notes.txt"))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        notes.Add(line);
                        notesListBox.Items.Add($"Note {notes.Count}");
                    }
                }

                MessageBox.Show("Notes loaded.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Notes file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void notesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Display the selected note in the TextBox
            int selectedIndex = notesListBox.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < notes.Count)
            {
                noteTextBox.Text = notes[selectedIndex];
            }
        }
    }
}
