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

        public Boolean           _wakeState;

        public Choices           _Phrases;

        public string            _executableLocation;

        public string            _namePath;

        public string            _exePath;

        public string            _inputPath;
    }

    

}