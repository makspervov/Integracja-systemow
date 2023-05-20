﻿namespace IS_Lab2_JSON;

using System.Text.Json.Serialization;

public class ShorterJST
{
	public int Kod_TERYT { get; set; }
	public string Województwo { get; set; }
	public string Powiat { get; set; }
	public string typ_JST { get; set; }
	public string nazwa_urzędu_JST { get; set; }
    public string miejscowość { get; set; }
    public int telefon_z_numerem_kierunkowym { get; set; }


    public ShorterJST(int kodTeryt, string województwo, string powiat, string typJst, string nazwaUrzęduJST, string miejscowość, int telefonZNumeremKierunkowym)
	{
		Kod_TERYT = kodTeryt;
		Województwo = województwo; 
		Powiat = powiat;
		typ_JST = typJst;
		nazwa_urzędu_JST = nazwaUrzęduJST;
        this.miejscowość = miejscowość;
        telefon_z_numerem_kierunkowym = telefonZNumeremKierunkowym;
    }
}
