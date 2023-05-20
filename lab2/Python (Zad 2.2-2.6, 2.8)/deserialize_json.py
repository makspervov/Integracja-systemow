# -*- coding: utf-8 -*-
"""
deserialize json
"""
import json

class DeserializeJson:
    # konstruktor
    def __init__(self, filename):
        print("let's deserialize something...")
        tempdata = open(filename, encoding="utf8")
        self.data = json.load(tempdata)

    # przykładowe statystyki
    # def somestats(self):
    #     example_stat = 0
    #     for dep in self.data:
    #         if dep['typ_JST'] == 'GM' and dep['Województwo'] == 'dolnośląskie': example_stat += 1
    #         print('liczba urzędów miejskich w województwie dolnośląskim: ' + ' ' + str(example_stat))

    # zadanie 2.3.3
    # modyfikacja funkcji somestats do wyświetlenia statystyki z każdego województwa
    def somestats(self):
        stats = {}
        for dep in self.data:
            jst_type = dep['typ_JST']
            wojewodztwo = dep['Województwo']
            if wojewodztwo not in stats:
                stats[wojewodztwo] = {}
            if jst_type not in stats[wojewodztwo]:
                stats[wojewodztwo][jst_type] = 0
            stats[wojewodztwo][jst_type] += 1

        for wojewodztwo, jst_types in stats.items():
            print('\nStatystyki dla województwa ' + wojewodztwo + ':')
            for jst_type, count in jst_types.items():
                print('\t- liczba urzędów typu ' + jst_type + ': ' + str(count))
