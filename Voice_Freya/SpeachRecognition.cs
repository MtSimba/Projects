using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Speech.Recognition;

namespace Voice_Freya
{
    public class SpeachRecognition
    {
        
        public void speachRecognition(object sender, SpeechRecognizedEventArgs e)
        {
            Freya f = new Freya();
            Speak s = new Speak();

            var executable = f._executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var wakeState = f._wakeState;
            var namePath = f._namePath = Path.Combine(executable, "name.txt");
            var exePath = f._exePath = Path.Combine(executable, "Voice_Freya.exe");
            var _s = s;
            var _f = f;

            string r = e.Result.Text;

            if (r == "wake")
            {
                _s.say("waking up");
                wakeState = true;
                //label3.Text = "state: awake";
            }

            if (r == "sleep")
            {
                _s.say("going to sleep");
                wakeState = false;
                //label3.Text = "state: asleep";

            }
            
            if (r == "restart" | r == "reboot")
            {
                _s.say("Restarting");
                // restart();
            }

            if (r == "hi" | r == "hello" | r == "greetings" | r == "Good day" | r == "salutations" | r == "hey" | r == "hello there") //What user says
            {
                if (r == "hello there")
                    _s.say("Ahh, general Kenobi.");
                else
                    _s.say(" Hi "); //What Freya says
            }

            if (r == "how are you?") //What user says
            {
                _s.say("I am fine, thanks for asking."); //What Freya says
            }

            if (r == "what time is it?") //What user says
            {
                _s.say(DateTime.Now.ToString("hh:mm tt")); //What Freya says
            }

            if (r == "what date is it?") //What user says
            {
                _s.say(DateTime.Now.ToString("M")); //What Freya says
            }

            if (r == "what is my name?" | r == "what's my name?")
            {
                _s.say("your name is " + File.ReadAllText(namePath) + ".");
            }

            if (r == "stop" | r == "freeze")
            {
                _f._voice.SpeakAsyncCancelAll();
            }

            if (r == "tell me a joke")
            {
                _s.say("your mama is so fat, Donald Trump used her as the border wall.");
            }

            if (r == "goodbye" | r == "bye" | r == "bye bye" | r == "farewell" | r == "good night")
            {
                _s.say("good bye");
                Environment.Exit(0);
            }

            if (r == "open google") //What user says
            {
                _s.say("okay");
                Process.Start("http://google.com"); //What Freya says
            }

            if (r == "open facebook") //What user says
            {
                _s.say("okay");
                Process.Start("http://facebook.com"); //What Freya says
            }

            if (r == "open youtube") //What user says
            {
                _s.say("okay");
                Process.Start("http://youtube.com"); //What Freya says
            }

            if (r == "open twitter") //What user says
            {
                _s.say("okay");
                Process.Start("http://twitter.com"); //What Freya says
            }

            if (r == "open Twitter") //What user says
            {
                _s.say("okay");
                Process.Start("http://twitter.com"); //What Freya says
            }


            Form1.textBox1.AppendText(r + " \n\n ");
        }

        /*public void restart()
        {
            Process.Start(exePath);
            Environment.Exit(0);
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
        }*/
    }
}