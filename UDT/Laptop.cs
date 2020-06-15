using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000, IsByteOrdered = true)]
public struct Laptop : INullable, IBinarySerialize
{
    //variable for firm of laptop
    private string _firm;
    //variable for model of laptop
    private string _model;
    //variable for serial number of laptop
    private string _serialNumber;

    private bool m_Null;
    //constructor
    public Laptop(string firm, string model, string serialNumber)
    {
        _firm = firm;
        _model = model;
        _serialNumber = serialNumber;
        m_Null = false;

    }
    //constructor
    public Laptop(bool nothing)
    {
        _model = _firm = "";
        _serialNumber = "";
        m_Null = true;
    }

    public override string ToString()
    {
        // Replace the following code with your code
        return "Laptop: "+_firm + ", " + _model + ", " + _serialNumber;
    }

    public bool IsNull
    {
        get
        {
            return m_Null;
        }
    }

    public static Laptop Null
    {
        get
        {
            Laptop h = new Laptop();
            h.m_Null = true;
            return h;
        }
    }

    //Method parses SqlString and create object
    public static Laptop Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        string[] data = s.Value.Split(",".ToCharArray());

        if (data.Length != 3) throw new ArgumentException("wprowadz poprawna ilosc argumentów");
        else if (data[0].Length < 1 || data[1].Length < 1 || data[2].Length < 1) throw new ArgumentException("Wprowadz porawna nazwe Modelu i Firmy i nrSeryjnego");


        return new Laptop(data[0], data[1], data[2]);

    }

    public void Write(System.IO.BinaryWriter w)
    {
        int maxStringSize = 50;
        string paddedString, temp;

        temp = _firm + "," + _model + "," + _serialNumber;
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
        string[] data = stringValue.Split(",".ToCharArray());

        this._firm = data[0];
        this._model = data[1];
        this._serialNumber = data[2];

    }

}


