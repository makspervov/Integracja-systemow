# -*- coding: utf-8 -*-
"""
json to yaml converter
"""

import json
import yaml

class ConvertJsonToYaml:
    @staticmethod
    def run(deserializeddata, yamlfilelocation):
        print("\nlet's convert something...")
        with open(yamlfilelocation, 'w', encoding='utf8') as outgoingfile:
            yaml.dump(deserializeddata, outgoingfile, allow_unicode=True)
        print("conversion is done!")

    @staticmethod
    def run(jsonfilelocation, yamlfilelocation):
        print("\nlet's convert something...")
        with open(jsonfilelocation, 'r', encoding='utf8') as incomingfile:
            deserializeddata = json.load(incomingfile)
        with open(yamlfilelocation, 'w', encoding='utf8') as outgoingfile:
            yaml.dump(deserializeddata, outgoingfile, allow_unicode=True)
        print("conversion is done!")
