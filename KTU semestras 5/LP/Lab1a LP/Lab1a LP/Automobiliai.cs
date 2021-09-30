using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab1a_LP
{
    class Automobiliai
    {
        Automobilis[] automobiliai;
        static object _lock = new Object();
        int memberCount = 0;
        public Automobiliai(int size)
        {
            automobiliai = new Automobilis[size];
        }

        public void insert(Automobilis auto)
        {
            Monitor.Enter(_lock);
            while(memberCount == arraySize())
            {
                Monitor.Wait(_lock);
            }
            for(int i = 0; i < arraySize(); i++)
            {
                if(automobiliai[i] == null)
                {
                    automobiliai[i] = auto;
                    break;
                }
                else if(automobiliai[i].CompareTo(auto) == -1)
                {
                    continue;
                }
                else
                {
                    for(int j = arraySize() - 1; j > i; j--)
                    {
                        automobiliai[j] = automobiliai[j - 1];
                    }
                    automobiliai[i] = auto;
                    break;
                }
            }
            memberCount++;
            Monitor.PulseAll(_lock);
            Monitor.Exit(_lock);
            
        }

        public void remove(int index)
        {
            Monitor.Enter(_lock);
            for(int i = index; i < arraySize() - 1; i++)
            {
                if(i == arraySize() - 1)
                {
                    automobiliai[i] = null;
                    break;
                }
                automobiliai[i] = automobiliai[i + 1];
            }
            memberCount--;
            Monitor.Exit(_lock);
        }

        public Automobilis getAuto()
        {
            Automobilis auto = null;
            lock (_lock)
            {
                while (memberCount == 0)
                {
                    Monitor.Wait(_lock);
                }
                auto = automobiliai[0];
                remove(0);

                if(memberCount > 0)
                {
                    Monitor.PulseAll(_lock);
                }
            }
            return auto;
        }

        public int arraySize()
        {
            return automobiliai.Length;
        }

        public int getMemberCount()
        {
            return memberCount;
        }
    }
}
