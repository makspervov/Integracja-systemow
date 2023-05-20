# -*- coding: utf-8 -*-
"""
deserialize xml
"""

import xml.etree.ElementTree as ET
import json

class DeserializeXML:
    @staticmethod
    def run(filelocation):
        print("\nLet's deserialize something...")
        tree = ET.parse(filelocation)
        root = tree.getroot()

        data = []
        for row in root.findall('row'):
            departament_data = {
                'Kod_TERYT': row.find('Kod_TERYT').text,
                'Województwo': row.find('Województwo').text,
                'Powiat': row.find('Powiat').text,
                'typ_JST': row.find('typ_JST').text,
                'nazwa_urzędu_JST': row.find('nazwa_urzędu_JST').text,
                'miejscowość': row.find('miejscowość').text,
                'telefon': int(row.find('telefon').text),
                'telefon_kierunkowy': int(row.find('telefon_kierunkowy').text)
            }
            data.append(departament_data)

        print("All data has been deserialized!")
        return {'data': data}

