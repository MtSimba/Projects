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
using System.Runtime.InteropServices;
using System.Reflection;

namespace Voice_Freya
{
    public partial class voiceFreya : Form
    {
        public voiceFreya()
        {
            SpeachRecognition speachRecognized = new SpeachRecognition();
            SpeechRecognitionEngine _recognition = new SpeechRecognitionEngine();
            Freya f = new Freya();

            var voice = f._voice = new SpeechSynthesizer();
            var phrases = f._Phrases = new Choices();
            var executableLocation = f._executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var namePath = f._namePath = Path.Combine(executableLocation, "name.txt");
            var exePath = f._exePath = Path.Combine(executableLocation, "Voice_Freya.exe");

            voice.SelectVoiceByHints(VoiceGender.Female); //Decide on what voice to use

            voice.Speak("Hello"); //printstatement to make Freya talk


            phrases.Add(new string[]
            {
                "hello", "how are you?","hi","greetings","good day", "salutations", "hey", "hello there",                                                //level 1 - introduktion phrases
                "tell me a joke", "what is my name?", "what's my name?"                                                
            });
            phrases.Add(new string[] {"goodbye","bye","bye bye","farewell","good night"});                                                       //level 1 - closing phrases
            phrases.Add(new string[] {"restart", "reboot", "stop","freeze"});                                                                            //level 1 - restarting phrase                                                                
            phrases.Add(new string[]
            {
                "what time is it?","what date is it?","what is the weather like?","what is the temperature outside?",                                //level 2 - time and place phrases
                "is it hot today?","is it cold today?"
            });
            phrases.Add(new string[]
            {
                "open google", "open facebook", "open youtube", "open twitter"                                                                        //level 2 - open and close web browser and applications phrases
                
            });
            phrases.Add(new string[]{"sleep","wake"});                                                                                           //level 1 - state changes phrases

            Grammar _grammar = new Grammar(new GrammarBuilder(phrases));
            try
            {
                _recognition.RequestRecognizerUpdate();
                _recognition.LoadGrammar(_grammar);
                _recognition.SpeechRecognized += speachRecognized.speachRecognition;
                _recognition.SetInputToDefaultAudioDevice();
                _recognition.RecognizeAsync(RecognizeMode.Multiple);
            }
            catch
            {
                return;
            }


            InitializeComponent();
        }


    }
}
