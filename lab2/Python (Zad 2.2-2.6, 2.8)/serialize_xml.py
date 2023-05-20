# -*- coding: utf-8 -*-
"""
serialize xml
"""

import xml.etree.ElementTree as ET

class SerializeXML:
    # metoda statyczna
    @staticmethod
    def run(deserializeddata, filelocation):
        print("\nlet's serialize something...")
        root = ET.Element("departaments")
        for dep in deserializeddata.data:
            dep_element = ET.SubElement(root, "JST")
            ET.SubElement(dep_element, "Kod_TERYT").text = str(dep['Kod_TERYT'])
            ET.SubElement(dep_element, "nazwa_samorządu").text = dep['nazwa_samorządu']
            ET.SubElement(dep_element, "Województwo").text = dep['Województwo']
            ET.SubElement(dep_element, "Powiat").text = dep['Powiat']
            ET.SubElement(dep_element, "typ_JST").text = dep['typ_JST']
            ET.SubElement(dep_element, "nazwa_urzędu_JST").text = dep['nazwa_urzędu_JST']
            ET.SubElement(dep_element, "miejscowość").text = dep['miejscowość']
            ET.SubElement(dep_element, "telefon_kierunkowy").text = str(dep['telefon kierunkowy'])
            ET.SubElement(dep_element, "telefon").text = str(dep['telefon'])
            ET.SubElement(dep_element, "email").text = dep['ogólny adres poczty elektronicznej gminy/powiatu/województwa']
            ET.SubElement(dep_element, "adres_www").text = dep['adres www jednostki']

        tree = ET.ElementTree(root)
        tree.write(filelocation, encoding='utf-8', xml_declaration=True)

        print("all data has been serialized!")


