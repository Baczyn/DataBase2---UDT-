using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.UserDefined, MaxByteSize = 8000, IsByteOrdered = true)]
public struct Phone : INullable, IBinarySerialize
{
    //variable for firm of phone
    private string _firm;
    //variable for model of phone
    private string _model;
    //variable for phone number
    private Int64 _nrPhone;

    private bool m_Null;
    //constructor
    public Phone(string firm, string model, Int64 nrPhone)
    {
        _firm = firm;
        _model = model;
        _nrPhone = nrPhone;
        m_Null = false;

    }
    //constructor
    public Phone(bool nothing)
    {
        _model = _firm = "";
        _nrPhone = 0;
        m_Null = true;
    }

    public override string ToString()
    {
        // Replace the following code with your code
        return "Phone: "+_firm + ", " + _model + ", " + _nrPhone.ToString();
    }
    //Method returns Phine number
    public string GetPhoneNumber() {
        return _nrPhone.ToString();
    }

    public bool IsNull
    {
        get
        {
            return m_Null;
        }
    }

    public static Phone Null
    {
        get
        {
            Phone h = new Phone();
            h.m_Null = true;
            return h;
        }
    }

    //method validates phone number(9-digits)
    public bool Validate(string nrPhone)
    {
        Int64 nr;
        if (nrPhone.Length != 9)
            return false;
        else if (Int64.TryParse(nrPhone, out nr) == false)
            return false;
        else if (nr < 0)
            return false;

        return true;



    }
    //Method parses SqlString and create object
    public static Phone Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;

        string[] data = s.Value.Split(",".ToCharArray());
        Phone phone = new Phone();

        if (data.Length != 3) throw new ArgumentException("wprowadz poprawna ilosc argumentów");
        else if (data[0].Length < 1 || data[1].Length < 1) throw new ArgumentException("Wprowadz porawna nazwe Modelu i Firmy");

        if (phone.Validate(data[2]) == false) throw new ArgumentException("wprowadz poprawna nrPhoneu");

        return new Phone(data[0], data[1], Int64.Parse(data[2]));
       
    }

    public void Write(System.IO.BinaryWriter w)
    {
        int maxStringSize = 50;
        string paddedString, temp;

        temp = _firm + "," + _model + "," + _nrPhone.ToString();
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
        this._nrPhone = Int64.Parse(data[2]);

    }




}


