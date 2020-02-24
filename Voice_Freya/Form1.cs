using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace Voice_Freya
{
    public partial class voiceFreya : Form
    {
        public voiceFreya()
        {
            
            SpeechRecognition speachRecognized = new SpeechRecognition();
            SpeechRecognitionEngine _recognition = new SpeechRecognitionEngine();
            Freya f = new Freya();

            var voice =        f._voice = new SpeechSynthesizer();                                                                      //Initialize the speechSynthesizer that makes the bot talk.
            var phrases =             f._Phrases = new Choices();                                                                              //Initialize the Choices that checks which inputs are "legal"
            var executableLocation =   f._executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);                 //Set executable location, so that namePath and exePath can access the files in \bin\debug
            var namePath =             f._namePath = Path.Combine(executableLocation, "name.txt");                                              //File path to the chosen name for "user"
            var exePath =              f._exePath = Path.Combine(executableLocation, "Voice_Freya.exe");                                        //File path to the "memory file", where all input is stored 

            voice.SelectVoiceByHints(VoiceGender.Female); //Decide on what voice/gender to use

            voice.Speak("Hello"); //Start statement to make bot talk


            phrases.Add(new string[]
            {
                "hello", "how are you?","hi","greetings","good day", "salutations", "hey", "hello there",                                                //level 1 - introduktion phrases
                "tell me a joke", "what is my name?", "what's my name?", "what time is it?", "what date is it?",
                "where are we?", "where am i?", "are you my friend?", "are we friends?", "do you know siri?",
                "do you know alexa?", "What's your favorite netflix show?", "What's your favorite netflix series?",

            });
            phrases.Add(new string[]
            {
                "goodbye","bye","bye bye","farewell","good night"                                                                                       //level 1 - closing phrases
            });                                                      
            phrases.Add(new string[]
            {
                "restart", "reboot", "stop","freeze"                                                                                                    //level 1 - restarting phrase   
            });                                                                                                                                         
            phrases.Add(new string[]
            {
                "open google", "open facebook", "open youtube", "open twitter", "close twitter"                                                           //level 2 - open and close web browser and applications phrases
                
            });
            phrases.Add(new string[]
            {
                "sleep","wake"                                                                                                                          //level 1 - state changes phrases
            });                                                                                           
            phrases.Add(new string[]
            {
                "wipe memory", "what have i told you?", "what's on your mind?","what have i told you?"                                                 //level 2 - "memory acess" phrases
            });
            phrases.Add(new string[]
            {
                "Do you like men?", "do you have a pussy?", "do you like women", "Do you wanna be my girlfriend?",
                "would you like to go on a date with me?" 
            });



            Grammar _grammar = new Grammar(new GrammarBuilder(phrases));
            try                                                             //try function that binds the grammar libary to the Speech recognizer.
            {
                _recognition.RequestRecognizerUpdate();
                _recognition.LoadGrammar(_grammar);
                _recognition.SpeechRecognized += speachRecognized.speechRecognition;
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
