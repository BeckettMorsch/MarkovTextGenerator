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
        private Dictionary<String, int> sums;
        private Random rand;

        public Chain ()
        {
            words = new Dictionary<String, List<Word>>();
            sums = new Dictionary<string, int>();
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
            sentence = new String(sentence.Where(c => !char.IsPunctuation(c)).ToArray());

            String[] words = sentence.Split(' ');

            // TODO: Add each word pair to the chain
            for (int i = 0; i < words.Length-1; i++)
            {
                AddPair(words[i], words[i + 1]);
            }
            AddPair(words[words.Length-1], "");

        }

        // Adds a pair of words to the chain that will appear in order
        public void AddPair(String word, String word2)
        {
            if (!words.ContainsKey(word))
            {
                sums.Add(word, 1);
                words.Add(word, new List<Word>());
                words[word].Add(new Word(word2));
            }
            else
            {
                bool found = false;
                foreach (Word s in words[word])
                {
                    if (s.ToString() == word2)
                    {
                        found = true;
                        s.Count++;
                        sums[word]++;
                    }
                }

                if (!found)
                {
                    words[word].Add(new Word(word2));
                    sums[word]++;
                }
            }
        }

        // Given a word, randomly chooses the next word
        public String GetNextWord (String word)
        {
            if (words.ContainsKey(word))
            {
                double choice = rand.NextDouble();

                //Console.WriteLine("I picked the number " + choice);

                List<Word> poss = words[word];

                double sum = 0;

                foreach( Word p in poss)
                {
                    sum += p.Probability;

                    if (sum > choice)
                        return p.ToString();
                }
            }

            return "idkbbq";
        }

        public void UpdateProbabilities ()
        {
            foreach (String word in words.Keys)
            {
                double sum = 0;  // Total sum of all the occurences of each followup word

                

                // Update the probabilities
                foreach (Word s in words[word])
                {
                    
                    s.Probability = (double)s.Count /
                        sums[word];
                }

            }
        }
    }
}
