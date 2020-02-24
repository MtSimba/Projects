using System.Speech.Synthesis;

namespace Voice_Freya
{
    public class Speak
    {
        Freya F = new Freya();
        public void say(string h)
        {
            var voice = F._voice = new SpeechSynthesizer();
            voice.SpeakAsync(h);
        }
        
    }
}