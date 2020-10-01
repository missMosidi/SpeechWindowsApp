using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.Diagnostics;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SpeechSynthesizer sSynth = new SpeechSynthesizer();
        PromptBuilder pBuilder = new PromptBuilder();
        SpeechRecognitionEngine sRecognize = new SpeechRecognitionEngine();



        private void button1_Click(object sender, EventArgs e)
        {
            pBuilder.ClearContent();
            pBuilder.AppendText(textBox1.Text);
            sSynth.Speak(pBuilder);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button2.Enabled = true;
            Choices sList = new Choices();
            sList.Add(new string[] {"hello","test","it works","how","are","you","today","i","am","fine","exit","close","quit","open chrome","what isthe current time" });
            //sSynth.SelectVoice(comboBox1.Text);
            Grammar gr = new Grammar(new GrammarBuilder(sList));

            try
            {
                sRecognize.RequestRecognizerUpdate();
                sRecognize.LoadGrammar(gr);
                sRecognize.SpeechRecognized += SRecognize_SpeechRecognized;
                sRecognize.SetInputToDefaultAudioDevice();
                sRecognize.RecognizeAsync(RecognizeMode.Multiple);


            }

            catch
            {
                return;
            }

            
        }

        private void SRecognize_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "exit" || e.Result.Text == "close" || e.Result.Text == "quit")
            {
                Application.Exit();
            }

            else
            {
                textBox1.Text = textBox1.Text + " " + e.Result.Text.ToString();
            }

            NewMethod(e);

        }

        private void NewMethod(SpeechRecognizedEventArgs e)
        {
            if (e.Result.Text == "open chrome")
            {
                Process.Start("chrome", "http://www.google.com");

            }
            else
            {
                textBox1.Text = textBox1.Text + " " + e.Result.Text.ToString();
            }
        }

        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    foreach (var voice in sSynth.GetInstalledVoices())
        //    {
        //         comboBox1.Items.Add(voice.VoiceInfo.Name);
        //    }
        //}

        private void button3_Click(object sender, EventArgs e)
        {

        }

        //private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        //{
           
        //}
    }
}
