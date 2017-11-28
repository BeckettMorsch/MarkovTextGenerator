using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkovTextGenerator
{
    class Chain
    {
        public Dictionary<String, List<Word>> words;
        private Random rand;

        public Chain ()
        {
            words = new Dictionary<String, List<Word>>();
            rand = new Random(System.Environment.TickCount);
        }

        // This may not be the best approach.. better may be to actually store
        // a separate list of actual sentence starting words and randomly choose from that
        public String GetRandomStartingWord ()
        {
            return words.Keys.ElementAt(rand.Next() % words.Keys.Count);
        }

        // Adds a sentence to the chain
        // You can use the empty string to indicate the sentence will end
        //
        // For example, if sentence is "The house is on fire" you would do the following:
        //  AddPair("The", "house")
        //  AddPair("house", "is")
        //  AddPair("is", "on")
        //  AddPair("on", "fire")
        //  AddPair("fire", "")

        public void AddString (String sentence)
        {
            // TODO: Break sentence up into word pairs
            sentence = new string(sentence.Where(c => !char.IsPunctuation(c)).ToArray());

            string[] words = sentence.Split(' ');

            // TODO: Add each word pair to the chain
            for (int i = 0; i < words.Length; i++)
            {
                AddPair(words[i], words[i + 1]);
            }
            AddPair(words[words.Length], "");

        }

        // Adds a pair of words to the chain that will appear in order
        public void AddPair(String word, String word2)
        {
            if (!words.ContainsKey(word))
            {
                words.Add(word, new List<Word>());
                words[word].Add(new Word(word2));
            }
            else
            {
                foreach (Word s in words[word])
                {
                    if (s.ToString() == word2)
                    {
                        s.Count++;
                    }
                }
            }
        }

        // Given a word, randomly chooses the next word
        public String GetNextWord (String word)
        {
            if (words.ContainsKey(word))
            {
                double choice = 1.0 / (double)rand.Next(100000);

                Console.WriteLine("I picked the number " + choice); 
            }

            return "idkbbq";
        }

        public void UpdateProbabilities ()
        {
            foreach (String word in words.Keys)
            {
                double sum = 0;  // Total sum of all the occurences of each followup word

                // Step 1:  Get the sum of all occurences of each followup word
                foreach (Word s in words[word])
                {
                    // TODO: Finish me


                }

                // Step 2:  Update the probabilities
                foreach (Word s in words[word])
                {
                    // TODO: Finish me
                }

            }
        }
    }
}
