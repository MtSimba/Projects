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
using System.Diagnostics;
using System.IO;
namespace Voice_Freya
{
    public partial class voiceFreya : Form
    {

        SpeechSynthesizer _voice = new SpeechSynthesizer();

        Boolean wakeState = true;

        Choices _Phrases = new Choices();

        private string username_path = @"D:\Repo\Private_Repo\Voice_Freya\name.txt";


        public voiceFreya()
        {
            _voice.SelectVoiceByHints(VoiceGender.Female); //Decide on what voice to use

            _voice.Speak("Hello"); //printstatement to make Freya talk

            SpeechRecognitionEngine _recognition = new SpeechRecognitionEngine();

            _Phrases.Add(new string[]
            {
                "hello", "how are you?","hi","greetings","good day", "salutations", "hey", "hello there",                                                //level 1 - introduktion phrases
                "tell me a joke", "what is my name?", "what's my name?"                                                
            });                           
            _Phrases.Add(new string[] {"goodbye","bye","bye bye","farewell","good night"});                                                           //level 1 - closing phrases
            _Phrases.Add(new string[] {"restart", "reboot", "stop"});                                                                            //level 1 - restarting phrase                                                                
            _Phrases.Add(new string[]
            {
                "what time is it?","what date is it?","what is the weather like?","what is the temperature outside?",                          //level 2 - time and place phrases
                "is it hot today?","is it cold today?"
            });                                                          
            _Phrases.Add(new string[]
            {
                "open google", "open facebook", "open youtube", "open twitter",                                        //level 2 - open and close web browser and applications phrases
                "open steam", "open origin", "open battle net", "open discord", "close discord",
                "close battle net","close steam", "close origin"
            });                                                      
            _Phrases.Add(new string[]{"sleep","wake"});                                                                                  //level 1 - state changes phrases

            Grammar _grammer = new Grammar(new GrammarBuilder(_Phrases));

            try
            {
                _recognition.RequestRecognizerUpdate();
                _recognition.LoadGrammar(_grammer);
                _recognition.SpeechRecognized +=_speachRecognized;
                _recognition.SetInputToDefaultAudioDevice();
                _recognition.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }


            InitializeComponent();
        }

        public static void killProg(string str)
        {
            Process[] procs = null;

            try 
            {
                procs = Process.GetProcessesByName(str);
                Process prog = procs[0];

                if (!prog.HasExited)
                {
                    prog.Kill();
                }
            }
            finally
            {
                if (procs != null)
                {
                    foreach (Process p in procs)
                    {
                        p.Dispose();
                    }
                }
            }
        }

        public void restart()
        {
            Process.Start(@"D:\Repo\Private_Repo\Voice_Freya\bin\Debug\Voice_Freya.exe");
            Environment.Exit(0);
        }

        public void say(string h)
        {
            _voice.SpeakAsync(h);
            textBox2.AppendText(h + " \n\n ");
        }

        private void _speachRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string r = e.Result.Text;
            
            if (r == "wake")
            {
                say("waking up");
                wakeState = true;
                label3.Text = "state: awake";
            }

            if (r == "sleep")
            {
                say("going to sleep");
                wakeState = false;
                label3.Text = "state: asleep";

            }
            
            if (wakeState == true)
            {

                if (r == "restart" | r == "reboot")
                {
                    say("Restarting");
                    restart();
                }

                if (r == "hi" | r == "hello" | r == "greetings" | r == "Good day" | r == "salutations" | r == "hey" | r == "hello there") //What user says
                {
                   if(r == "hello there")
                       say("Ahh, general Kenobi");
                   else
                     say(" Hi "); //What Freya says
                }

                if (r == "how are you?") //What user says
                {
                    say("I am fine, thanks for asking."); //What Freya says
                }

                if (r == "what time is it?") //What user says
                {
                    say(DateTime.Now.ToString("hh:mm tt")); //What Freya says
                }

                if (r == "what date is it?") //What user says
                {
                    say(DateTime.Now.ToString("M")); //What Freya says
                }

                if (r == "what is my name?" | r == "what's my name?")
                {
                    say("your name is " + File.ReadAllText(username_path) + ".");
                }

                if (r == "stop")
                {
                    _voice.SpeakAsyncCancelAll();
                }

                if (r == "tell me a joke")
                {
                    say("your mama is so fat, Donald Trump used her as the border wall.");
                }

                if (r == "goodbye" | r == "bye" | r == "bye bye" | r == "farewell" | r == "good night")
                {
                    say("good bye");
                    Environment.Exit(0);
                }

                if (r == "open google") //What user says
                {
                    say("okay");
                    Process.Start("http://google.com"); //What Freya says
                }

                if (r == "open Facebook") //What user says
                {
                    say("okay");
                    Process.Start("http://facebook.com"); //What Freya says
                }

                if (r == "open youtube") //What user says
                {
                    say("okay");
                    Process.Start("http://youtube.com"); //What Freya says
                }

                if (r == "open Twitter") //What user says
                {
                    say("okay");
                    Process.Start("http://twitter.com"); //What Freya says
                }

                if (r == "open Twitter") //What user says
                {
                    say("okay");
                    Process.Start("http://twitter.com"); //What Freya says
                }

                if (r == "open steam") //What user says
                {
                    say("okay");
                    Process.Start(@"C:\Program Files (x86)\Steam\Steam.exe"); // What Freya says
                }

                if (r == "open origin") //What user says
                {
                    say("okay"); 
                    Process.Start(@"D:\Games\Origin\Origin.exe"); // What Freya says
                }

                if (r == "open battle net") //What user says
                {
                    say("okay"); 
                    Process.Start(@"D:\Games\Blizzard\Battle.net\Battle.net Launcher.exe");
                }

                if (r == "close battle net") //What user says
                { 
                    say("understood."); // What Freya says
                    killProg("Battle.net");
                }

                if (r == "open discord") //What user says
                {
                    say("okay");
                    Process.Start(@"C:\Users\Victo\AppData\Local\Discord\app-0.0.305\Discord.exe"); // What Freya says
                }

                if (r == "close discord") //What user says
                {
                    say("understood.");
                    killProg("Discord");
                }


            }
            textBox1.AppendText(r + " \n\n ");
        }

    }
}
