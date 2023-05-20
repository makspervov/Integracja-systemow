#
"""
serialize json
"""
import json

class SerializeJson:
    # metoda statyczna
    @staticmethod
    def run(deserializeddata, filelocation):
        print("\nlet's serialize something...")
        lst = []
        # TODO – do modyfikacji
        for dep in deserializeddata.data:
            lst.append({
                "Kod_TERYT": dep['Kod_TERYT'],
                "Województwo": dep['Województwo'],
                "Powiat": dep['Powiat'],
                "typ_JST": dep['typ_JST'],
                "nazwa_urzędu_JST": dep['nazwa_urzędu_JST'],
                "miejscowość": dep['miejscowość'],
                "telefon": dep['telefon'],
                "telefon_kierunkowy": dep['telefon kierunkowy']
            })
        jsontemp = {"departaments": lst}
        with open(filelocation, 'w', encoding='utf-8') as f:
            json.dump(jsontemp, f, ensure_ascii=False)
        print("all data has been serialized!")

    def run_xml(deserializeddata, filelocation):
        print("\nLet's serialize something...")
        lst = []
        for dep in deserializeddata['data']:
            lst.append({
                "Kod_TERYT": dep['Kod_TERYT'],
                "Województwo": dep['Województwo'],
                "Powiat": dep['Powiat'],
                "typ_JST": dep['typ_JST'],
                "nazwa_urzędu_JST": dep['nazwa_urzędu_JST'],
                "miejscowość": dep['miejscowość'],
                "telefon_kierunkowy": dep['telefon_kierunkowy']
            })
        jsontemp = {"JST": lst}
        with open(filelocation, 'w', encoding='utf-8') as f:
            json.dump(jsontemp, f, ensure_ascii=False)
        print("All data has been serialized!")