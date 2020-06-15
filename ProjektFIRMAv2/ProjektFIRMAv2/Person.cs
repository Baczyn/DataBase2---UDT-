using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000, IsByteOrdered = true)]
public struct Person : INullable, IBinarySerialize
{
    //varianle for surname
    private string _surname;
    //varianle for name
    private string _name;
    //varianle for sex
    private string _sex;
    //varianle for pesel
    private Int64 _pesel;
    private bool is_Null;
    //constructor
    public Person(string surname, string name, string sex, Int64 pesel)
    {
        _surname = surname;
        _name = name;
        _sex = sex;
        _pesel = pesel;
        is_Null = false;
    }
    //constructor
    public Person(bool nothing)
    {
        _surname = _name = _sex = "";
        _pesel = 0;
        is_Null = true;
    }

    public override string ToString()
    {
        return "Person: "+_surname + ", " + _name + ", " + _sex + ", " + _pesel.ToString();
    }
    //Method returns _surname
    public string GetSurname()
    {
        return _surname;
    }
    //Method returns _pesel
    public string GetPesel() {
        return _pesel.ToString();
    }
    //method validates sex('kobieta' or 'mezczyzna')
    public bool ValidateSex(string sex)
    {

        if (sex.Equals("kobieta") || sex.Equals("mezczyzna"))
            return true;
        else
            return false;

    }
    //method validates pesel (11 digits - yy/mm/dd/  and 9'th must be '0' if man or '1'if woman)
    public bool ValidatePesel(string pesel, string sex)
    {
        long number;
        if (pesel.Length != 11)
            return false;
        else if (!Int64.TryParse(pesel, out number))
            return false;
        else if ((pesel[8]-'0') % 2 != 0 && sex.Equals("kobieta"))
            return true;
        else if ((pesel[8] - '0') % 2 == 0 && sex.Equals("mezczyzna"))
            return true;
        else
            return false;
    }

    public bool IsNull
    {
        get
        {
            return is_Null;
        }
    }

    public static Person Null
    {
        get
        {
            Person Person = new Person();
            Person.is_Null = true;
            return Person;
        }
    }

    //Method parses SqlString and create object
    [SqlMethod(OnNullCall = false)]
    public static Person Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        Person person = new Person();

        string[] data = s.Value.Split(",".ToCharArray());
        if (data.Length < 4) throw new ArgumentException("Za mała ilość argumentów");

        string sex = data[2];

        sex = sex.ToLower();
        if (person.ValidateSex(sex) == false) throw new ArgumentException("Podaj poprawna plec");

        if (person.ValidatePesel(data[3], sex) == false) throw new ArgumentException("Podaj poprawna pesel");

        return new Person(data[0], data[1], sex, Int64.Parse(data[3]));
    }

    public void Write(System.IO.BinaryWriter w){
        int maxStringSize = 50;
        string paddedString,temp;

        temp = _surname + ',' + _name + ',' + _sex + ',' + _pesel.ToString();
        paddedString = temp.PadRight(maxStringSize, '\0');

        for (int i = 0; i < paddedString.Length; i++)
            w.Write(paddedString[i]);
   
   }

    public void Read(System.IO.BinaryReader r){
        int maxStringSize = 50;
        char[] chars;
        int stringEnd;
        string stringValue;
        chars = r.ReadChars(maxStringSize);
        stringEnd = Array.IndexOf(chars, '\0');
        if (stringEnd == 0)
        {
            stringValue = null;
            return;
        }
        stringValue = new String(chars, 0, stringEnd);
        string[] data = stringValue.Split(",".ToCharArray());

        this._surname = data[0];
        this._name = data[1];
        this._sex = data[2];
        this._pesel = Int64.Parse(data[3]);



    }
  


}


