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

namespace Voice_Freya
{
    public partial class voiceFreya : Form
    {

        SpeechSynthesizer _voice = new SpeechSynthesizer();

        Boolean wakeState = false;

        Choices _Phrases = new Choices();
        
        public voiceFreya()
        {
            _voice.SelectVoiceByHints(VoiceGender.Female); //Decide on what voice to use

            _voice.Speak("Hello"); //printstatement to make Freya talk

            SpeechRecognitionEngine _recognition = new SpeechRecognitionEngine();

            _Phrases.Add(new string[]
            {
                "hello freya", "how are you?","hi freya","greetings freya","good day freya", "salutations freya", "hey freya"                                                             //level 1 - introduktion phrases
            });                           
            _Phrases.Add(new string[] {"goodbye","bye","bye bye","farewell","good night"});                                                           //level 1 - closing phrases
            _Phrases.Add(new string[] {"Restart", "reboot"});                                                                            //level 1 - restarting phrase                                                                
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
            _voice.Speak(h);
            wakeState = false;
            textBox2.AppendText(h + " \n\n ");
        }

        private void _speachRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string r = e.Result.Text;

            if (r == "hi freya" | r == "hello freya" | r == "greetings freya" | r == "Good day freya" |
                r == "salutations freya" | r == "hey freya")
            {
                wakeState = true;
            }


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


                if (r == "hi" | r == "hello" | r == "greetings" | r == "Good day") //What user says
                {
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
                    say(DateTime.Now.ToString("yy-MMM-dd")); //What Freya says
                }

                if (r == "goodbye" | r == "bye" | r == "bye bye" | r == "farewell" | r == "good night")
                {
                    say("good bye");
                    Environment.Exit(0);
                }

                if (r == "freya open google") //What user says
                {
                    say("okay");
                    Process.Start("http://google.com"); //What Freya says
                }

                if (r == "freya open Facebook") //What user says
                {
                    say("okay");
                    Process.Start("http://facebook.com"); //What Freya says
                }

                if (r == "freya open youtube") //What user says
                {
                    say("okay");
                    Process.Start("http://youtube.com"); //What Freya says
                }

                if (r == "freya open Twitter") //What user says
                {
                    say("okay");
                    Process.Start("http://twitter.com"); //What Freya says
                }

                if (r == "freya open Twitter") //What user says
                {
                    say("okay");
                    Process.Start("http://twitter.com"); //What Freya says
                }

                if (r == "freya open steam")
                {
                    say("okay");
                    Process.Start(@"C:\Program Files (x86)\Steam\Steam.exe");
                }

                if (r == "freya open origin")
                {
                    say("okay");
                    Process.Start(@"D:\Games\Origin\Origin.exe");
                }

                if (r == "freya open battle net")
                {
                    say("okay");
                    Process.Start(@"D:\Games\Blizzard\Battle.net\Battle.net Launcher.exe");
                }

                if (r == "freya close battle net")
                {
                    say("understood.");
                    killProg("Battle.net");
                }

                if (r == "freya open discord")
                {
                    say("okay");
                    Process.Start(@"C:\Users\Victo\AppData\Local\Discord\app-0.0.305\Discord.exe");
                }

                if (r == "freya close discord")
                {
                    say("understood.");
                    killProg("Discord");
                }


            }
            textBox1.AppendText(r + " \n\n ");
        }

    }
}
