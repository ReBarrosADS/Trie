using System;
using System.Collections.Generic;

public class Noh
{
    public Noh()
    {
        Filho = new Dictionary<char, Noh>();
    }

    public Dictionary<char, Noh> Filho { get; set; }
    public bool FimPalavra { get; set; }
}

public class Trie
{
    public Trie()
    {
        Raiz = new Noh();
    }

    public Noh Raiz { get; private set; }

    // Método para inserir uma palavra na Trie
    public void Insert(string word)
    {
        Noh current = Raiz;
        foreach (char c in word)
        {
            if (!current.Filho.ContainsKey(c))
            {
                current.Filho[c] = new Noh();
            }
            current = current.Filho[c];
        }
        current.FimPalavra = true;
    }

    // Método para buscar uma palavra na Trie
    public bool Search(string word)
    {
        Noh node = GetNode(word);
        return node != null && node.FimPalavra;
    }

    // Método para verificar se existe alguma palavra na Trie com um determinado prefixo
    public bool StartsWith(string prefix)
    {
        return GetNode(prefix) != null;
    }

    // Método auxiliar para obter o nó correspondente a uma palavra ou prefixo
    private Noh GetNode(string word)
    {
        Noh current = Raiz;
        foreach (char c in word)
        {
            if (!current.Filho.ContainsKey(c))
            {
                return null;
            }
            current = current.Filho[c];
        }
        return current;
    }

    // Método para deletar uma palavra da Trie
    public bool Delete(string word)
    {
        return Delete(Raiz, word, 0);
    }

    private bool Delete(Noh current, string word, int index)
    {
        if (index == word.Length)
        {
            if (!current.FimPalavra)
            {
                return false;
            }
            current.FimPalavra = false;
            return current.Filho.Count == 0;
        }

        char c = word[index];
        if (!current.Filho.ContainsKey(c))
        {
            return false;
        }

        bool shouldDeleteCurrentNode = Delete(current.Filho[c], word, index + 1);

        if (shouldDeleteCurrentNode)
        {
            current.Filho.Remove(c);
            return current.Filho.Count == 0 && !current.FimPalavra;
        }

        return false;
    }
}

class Program
{
    static void Main()
    {
        Trie trie = new Trie();

        trie.Insert("banana");
        trie.Insert("barco");
        trie.Insert("banco");
        trie.Insert("casa");
        trie.Insert("casaco"); 
        trie.Insert("celta");
        trie.Insert("cola");

        Console.WriteLine(trie.Search("banana")); // True
        Console.WriteLine(trie.Search("bar")); // False
        Console.WriteLine(trie.StartsWith("banco")); // True
        Console.WriteLine(trie.StartsWith("casa")); // True
        Console.WriteLine(trie.StartsWith("casar")); // False

        trie.Delete("casa");
        Console.WriteLine(trie.Search("casa")); // False

        trie.Delete("barco");
        Console.WriteLine(trie.Search("barco")); // False
    }
}