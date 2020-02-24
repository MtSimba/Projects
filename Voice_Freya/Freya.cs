using System;
using System.IO;
using System.Reflection;
using System.Speech.Recognition;
using System.Speech.Synthesis;

namespace Voice_Freya
{
    public struct Freya
    {
        public SpeechSynthesizer _voice;

        public Boolean _wakeState;

        public Choices _Phrases;

        public string _executableLocation;

        public string _namePath;

        public string _exePath;

        public Freya(SpeechSynthesizer voice, Boolean wakeState, Choices Phrases, string executableLocation, string namePath, string exePath)
        {
            _voice = voice;
            _voice = new SpeechSynthesizer();

            _wakeState = wakeState;

            _Phrases = Phrases;
            _Phrases = new Choices();

            _executableLocation = executableLocation;
            _executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            _namePath = namePath;
            _namePath = Path.Combine(executableLocation, "name.txt");

            _exePath = exePath;
            _exePath = Path.Combine(executableLocation, "Voice_Freya.exe");

        }
    }

    

}