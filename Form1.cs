using System;// using system
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.IO;
using System.Diagnostics;
using System.Speech;



namespace VoiceCommand
{
    public partial class Form1 : Form // eto for structure
    {

        #region Eto yung mga libraries na tinawag natin
        // eto yung importat para ma access natin yung functionalities ng speech recognition
        // eto yung mga naka declared sa taas System.Speech recognition, System.SpeechSynthesis
        SpeechRecognitionEngine _recognizer = new SpeechRecognitionEngine();
        SpeechSynthesizer baste = new SpeechSynthesizer();
        SpeechRecognitionEngine startlistening = new SpeechRecognitionEngine();



        Random rnd = new Random(); // for accesing the settings
        int RecTime = 0; // this will help the system to run kung ano lang yung nasa list
        DateTime TimeNow = DateTime.Now;
        



        #endregion

        public Form1() //for structure
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            BackColor = Color.Transparent;
            InitializeComponent();
            baste.SpeakAsync("Welcome back user! I hope you are feeling good today!");
        }

        private void Form1_Load(object sender, EventArgs e) //for structure method
        {
            _recognizer.SetInputToDefaultAudioDevice(); // eto yung responsible sa audio at microphone na gagamitin sa ating system
            _recognizer.LoadGrammarAsync(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DefaultCommands.txt")))));
                                                                                         //nilagay ng methods
            _recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(Default_SpeechRecognized);
                                                                                        //nilagay sa methods
            _recognizer.SpeechDetected += new EventHandler<SpeechDetectedEventArgs>(_recognizer_SpeechRecognized);
           
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);

            startlistening.SetInputToDefaultAudioDevice();
            startlistening.LoadGrammar(new Grammar(new GrammarBuilder(new Choices(File.ReadAllLines(@"DefaultCommands.txt")))));

                                                                                              //nilagay sa methods            
            startlistening.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(startlistening_SpeechRecognized);
          
        }



        private void Default_SpeechRecognized(object sender, SpeechRecognizedEventArgs e) // for structure method
        {
            // this will be for our commands
            int ranNum;
            string speech = e.Result.Text;

            if (speech == "hello")
            {
                baste.SpeakAsync("How are you sir!");
            }

            if (speech == "motivate me codex")
            {
                baste.SpeakAsync(quotes());
            }


            if (speech == "stop talking")
            {
                baste.SpeakAsyncCancelAll();
                ranNum = rnd.Next(1,2);    //ranNum is an integer

                if (ranNum == 1)
                {
                    baste.SpeakAsync("Yes sir");
                }

                if (ranNum == 2)
                {
                    baste.SpeakAsync("Im sorry");
                }
            }

            if (speech == "command list")
            {
                baste.Speak("here are my commands");
                WindowState = FormWindowState.Normal;
                Form cmd = new commands();
                cmd.Show();

            }
            if (speech == "stop listening")
            {
                baste.SpeakAsync("If you need me just say, WAKE UP CODEX!");
                _recognizer.RecognizeAsyncCancel();
                startlistening.RecognizeAsync(RecognizeMode.Multiple);
            }

            //switch statements

            switch (speech)
            {
                //greetings
                case("hi"):
                    baste.SpeakAsync("Hello user");
                    break;
                case ("what is your name"):
                    baste.SpeakAsync("good to see you my name is, Codex, I'm a voice assistant");
                    break;
                case ("hey"):
                    baste.SpeakAsync("hey, what's up?");
                    break;
                case ("your history"):
                    baste.SpeakAsync("My name is Codex. I was created by Sebastian's group for their final project in intermediate programming. I'm so blessed that they chose to make a voice command. " +
                        "Such a wonderful journey for all of you guys. I will miss you all.");
                    break;

                //windows functionalities
                case ("close"):
                    baste.Speak("closing tab");
                    SendKeys.Send("^{W}");
                    break;
                case ("exit"):
                    baste.Speak("closing application");
                    SendKeys.Send("%{F4}");                 
                    break;
                case ("bye"):
                    baste.Speak("shutting down in, three, two, one, goodbye user, always remember that all is well");
                    this.Close();
                    break;
                case ("hide"):
                    baste.Speak("my bad. sorry sir");
                    WindowState = FormWindowState.Minimized;
                    break;

                case ("show"):
                    baste.Speak("I'm back!");
                    WindowState = FormWindowState.Normal;
                    break;

                //time and date

                case ("time today"):
                    baste.Speak("The time today is" + DateTime.Now.ToShortTimeString());
                    break;

                case ("date today"):
                    baste.Speak("Today is" + DateTime.Now.ToShortDateString());
                    break;


                //software and applications
                case ("open word"):
                    baste.Speak("Opening ms word");
                    Process.Start("WINWORD.EXE");
                    break;

                case ("excel"):
                    baste.Speak("opening excel");
                    Process.Start("EXCEL.EXE");
                    break;

                case ("power point"):
                    baste.Speak("opening power point");
                    Process.Start("POWERPNT.EXE");
                    break;
                case ("paint"):
                    baste.Speak("opening paint, practicing your art skill is very cool!");
                    Process.Start("mspaint.exe");
                    break;

                //website

                case ("facebook"):
                    baste.Speak("opening facebook, always secure your account in any hacker");
                    Process.Start("https://www.facebook.com/"); 
                    break;
                case ("gmail"):
                    baste.Speak("always secure your email account");
                    Process.Start("https://mail.google.com/");
                    break;
                case ("youtube"):
                    baste.Speak("opening youtube, You can always save some time to relax yourself");
                    Process.Start("https://www.youtube.com/");
                    break;

                //shortcut keys
                case ("capture"):
                    baste.Speak("capturing image");
                    SendKeys.Send("{PRTSC}");
                    break;

                case ("paste"):
                    baste.Speak("Pasting");
                    SendKeys.Send("^{V}");
                    break;

                case ("select"):
                    baste.Speak("okay sir");
                    SendKeys.Send("{ENTER}");
                    break;

                case ("next"):
                    SendKeys.Send("{RIGHT}");
                    break;
                case ("back"):
                    SendKeys.Send("{LEFT}");
                    break;

                case ("go for"):
                    baste.Speak("This?");
                    SendKeys.Send("{TAB}");
                    break;

                case ("erase"):
                    SendKeys.Send("{BKSP}");
                    break;
                case ("down"):
                    SendKeys.Send("{DOWN}");
                    break;
                case ("up"):
                    SendKeys.Send("{UP}");
                    break;
                case ("write"):
                    string ch = speech;
                    baste.Speak("typing");
                    SendKeys.Send(ch.ToString());
                    break;
                case ("tab"):
                    baste.Speak("choose tab");
                    SendKeys.Send("%{TAB}");
                    break;
               
            }

        }



        //All answer choices para di na kakaumay paulit ulit yung system

        #region         
           //structure
        public string name() //this code is responsible if you ask the software's name
        {
            string[] greetings = new string[5] { "Hello my name is, Codex!  ", "ohh I am, Codex!", "Good to see you my name is, Codex!",
                  "I am, Codex! ", "Good day! my name is, Codex! " };
            Random r = new Random();
            return greetings[r.Next(5)];
        }

        public string quotes() //this code is responsible if you ask the software's name
        {
            string[] greetings = new string[6] { "You can't win at everything but you can try", "Life isn't beautiful; it only becomes beautiful, once you start making it one",
                "life doesn't give you what you want. Life gives you what you deserve","Many of life's failures are people who did not realize how close they were to success when they gave up.",
                "The best way to predict your future is to create it.", "i'll leave tomorrows problems to tomorrows me" };
            Random r = new Random();
            return greetings[r.Next(6)];
        }





        #endregion


        private void _recognizer_SpeechRecognized(object sender, SpeechDetectedEventArgs e)
        {
            RecTime = 0;
        }



     

        private void startlistening_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string speech = e.Result.Text;
            if (speech == "wake up codex")
            {
                startlistening.RecognizeAsyncCancel();
                baste.SpeakAsync("ohhh, I'm back! What can I do for you?");
                _recognizer.RecognizeAsync(RecognizeMode.Multiple);
            }
        }




        private void LstCommand_TextChanged(object sender, EventArgs e)
        {

        }




        private void timer1_Tick(object sender, EventArgs e)
        {
            if (RecTime == 10) // responsible sa range ng time kung kailan lang mag accept ng mga commands ang software
            {

            }
            else if (RecTime == 11) 
            {
                Timer.Stop();
                startlistening.RecognizeAsync(RecognizeMode.Multiple);
                RecTime = 0;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SpeechSynthesizer s = new SpeechSynthesizer();
            string a;
            a = richTextBox1.Text;
            s.Speak(a);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Text = File.ReadAllText(openFileDialog1.FileName);
                this.Text = openFileDialog1.SafeFileName;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            /*WindowState = FormWindowState.Maximized;
            Form cmd = new commands();
            cmd.Show();*/

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Form cmd = new commands();
            cmd.Show();
        }
    }
}
