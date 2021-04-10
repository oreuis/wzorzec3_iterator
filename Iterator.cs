using System;
using System.Collections;
using System.Collections.Generic;

namespace wzorzec3
{
    abstract class WzIter : IEnumerator
    {
        object IEnumerator.Current => Current();

        public abstract int Key();

        public abstract object Current();

        public abstract bool MoveNext();

        public abstract void Reset();
    }

    abstract class klasa1 : IEnumerable
    {
        public abstract IEnumerator GetEnumerator();
    }
    class klasa2 : WzIter
    {
        private kolekcja kol;
        private int kolekcja_poz = -1;

        private bool rev = false;

        public klasa2(kolekcja kolek, bool reve = false)
        {
            this.kol = kolek;
            this.rev = reve;

            if (reve)
            {
                this.kolekcja_poz = kolek.getItems().Count;
            }
        }

        public override object Current()
        {
            return this.kol.getItems()[kolekcja_poz];
        }

        public override int Key()
        {
            return this.kolekcja_poz;
        }

        public override bool MoveNext()
        {
            int new_poz = this.kolekcja_poz + (this.rev ? -1 : 1);

            if (new_poz >= 0 && new_poz < this.kol.getItems().Count)
            {
                this.kolekcja_poz = new_poz;
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void Reset()
        {
            this.kolekcja_poz = this.rev ? this.kol.getItems().Count - 1 : 0;
        }
    }

    class kolekcja : klasa1
    {
        List<string> moja_kol = new List<string>();

        bool rev_kier = false;

        public void ReverseDirection()
        {
            rev_kier = !rev_kier;
        }

        public List<string> getItems()
        {
            return moja_kol;
        }

        public void AddItem(string item)
        {
            this.moja_kol.Add(item);
        }

        public override IEnumerator GetEnumerator()
        {
            return new klasa2(this, rev_kier);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var albumy = new kolekcja();
            albumy.AddItem("17Carat");
            albumy.AddItem("Boys Be");
            albumy.AddItem("Love Letter");
            albumy.AddItem("Teenage");
            albumy.AddItem("All");
            albumy.AddItem("Director's Cut");
            albumy.AddItem("You Make My Day");
            albumy.AddItem("You Made My Dawn");
            albumy.AddItem("An Ode");
            albumy.AddItem("Heng:garae");
            albumy.AddItem("Semicolon;");

            Console.WriteLine("Albumy dodane od najstarszego do najnowszego:");

            foreach (var element in albumy)
            {
                Console.WriteLine(element);
            }

            Console.WriteLine("\nAlbumy dodane od najnowszego do najstarszego:");

            albumy.ReverseDirection();

            foreach (var element in albumy)
            {
                Console.WriteLine(element);
            }
        }
    }
}
