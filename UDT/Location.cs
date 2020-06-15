using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000, IsByteOrdered = true)]
public struct Location : INullable, IBinarySerialize
{   
    //variable for city
    private string _city;
    //variable for street
    private string _street;
    //variable for flat number
    private Int32 _nr;
    //variable for postcode
    private Int32 _postCode;

    private bool m_Null;
    //constructor
    public Location(string city, string street, Int32 nr, Int32 postCode) {
        _city = city;
        _street = street;
        _nr = nr;
        _postCode = postCode;
        m_Null = false;
    
    }
    //constructor
    public Location(bool nothing)
    {
        _street = _city = "";
        _nr=_postCode = 0;
        m_Null = true;
    }

    public override string ToString()
    {
        return "Address: st:"+_street + ", nr:" +_nr.ToString() + ", "+ _city + ", postcode:"+_postCode.ToString();
    }

    public bool IsNull
    {
        get
        {
            return m_Null;
        }
    }

    public static Location Null
    {
        get
        {
            Location h = new Location();
            h.m_Null = true;
            return h;
        }
    }
    //Method validats falt number(int > 0)and postode(5 digits)
    public bool Validate(string nr, string postCode) { 
        Int32 intNr;
        Int32 code;
        if (Int32.TryParse(nr, out intNr) == false)
            return false;
        else if (intNr < 0)
            return false;
        if (postCode.Length != 5)
            return false;
        else if (Int32.TryParse(postCode, out code) == false)
            return false;

        return true;
        


    }
    //Method parses SqlString and create object
    public static Location Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        string[] dane = s.Value.Split(",".ToCharArray());
        Location loc = new Location();

        if(dane.Length!=4) throw new ArgumentException("wprowadz poprawna ilosc argumentów");
        else if(dane[0].Length < 1 || dane[1].Length < 1)throw new ArgumentException("Wprowadz porawna nazwe miasta i ulicy");
        
        if(loc.Validate(dane[2],dane[3]) == false) throw new ArgumentException("wprowadz poprawna nr oraz postCode");

        return new Location(dane[0], dane[1], Int32.Parse(dane[2]), Int32.Parse(dane[3]));

    }

    public void Write(System.IO.BinaryWriter w)
    {
        int maxStringSize = 50;
        string paddedString, temp;

        temp = _street + "," +_nr.ToString() + ","+ _city + ","+_postCode.ToString();
        paddedString = temp.PadRight(maxStringSize, '\0');

        for (int i = 0; i < paddedString.Length; i++)
            w.Write(paddedString[i]);

    }

    public void Read(System.IO.BinaryReader r)
    {
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
        string[] dane = stringValue.Split(",".ToCharArray());

        this._city = dane[2];
        this._street = dane[0];
        this._nr = Int32.Parse(dane[1]);
        this._postCode = Int32.Parse(dane[3]);



    }

   

    
}


