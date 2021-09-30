using System;

namespace Kolis
{
    class MacbookComputer
    {
        public virtual string videoOutput()
        {
            return "MacbookVaizdas";
        }
    }

    class USBCProjector : MacbookComputer
    {
        Adaptee adaptee;

        public USBCProjector(Adaptee adaptee)
        {
            this.adaptee = adaptee;
        }

        public override string videoOutput()
        {
            return adaptee.getRefreshRate() + " " + adaptee.getResolution();
        }
    }

    abstract class Adaptee
    {
        public abstract string getResolution();
        public abstract string getRefreshRate();
    }

    class VGAProjector : Adaptee
    {
        public override string getResolution()
        {
            return "VGAProjector resolution";
        }

        public override string getRefreshRate()
        {
            return "VGAProjector refresh rate";
        }
    }

    class HDMIProjector : Adaptee
    {
        public override string getResolution()
        {
            return "HDMIProjector resolution";
        }

        public override string getRefreshRate()
        {
            return "HDMIProjector refresh rate";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MacbookComputer macWithVGA= new USBCProjector(new VGAProjector());
            MacbookComputer macWithHDMI = new USBCProjector(new HDMIProjector());

            Console.WriteLine(macWithVGA.videoOutput());
            Console.WriteLine(macWithHDMI.videoOutput());
        }
    }
}
