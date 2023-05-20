print("\nhey, it's me - Python!")

# print("Zadania 2.3-2.5")
# from deserialize_json import DeserializeJson
# newDeserializator = DeserializeJson('Assets/data.json')
# newDeserializator.somestats()
#
# from serialize_json import SerializeJson
# SerializeJson.run(newDeserializator, 'Assets/data_mod.json')
#
# from convert_json_to_yaml import ConvertJsonToYaml
# ConvertJsonToYaml.run('Assets/data.json', 'Assets/data_mod.yaml')
#
# print("Zadanie 2.6")
# import yaml
# tempconffile = open('Assets/basic_config.yaml', encoding='utf8')
# confdata = yaml.load(tempconffile, Loader=yaml.FullLoader)
#
# from deserialize_json import DeserializeJson
# newDeserializator = DeserializeJson(confdata['paths']['source_folder']+confdata['paths']['json_source_file'])
# newDeserializator.somestats()
#
# from serialize_json import SerializeJson
# SerializeJson.run(newDeserializator, confdata['paths']['source_folder']+confdata['paths']['json_destination_file'])
#
# from convert_json_to_yaml import ConvertJsonToYaml
# ConvertJsonToYaml.run(confdata['paths']['source_folder']+confdata['paths']['json_source_file'],
#                       confdata['paths']['source_folder']+confdata['paths']['yaml_destination_file'])

# Zadanie 2.6
# import yaml
#
# tempconffile = open('Assets/basic_config.yaml', encoding='utf8')
# confdata = yaml.load(tempconffile, Loader=yaml.FullLoader)
#
# if confdata['operations']['deserialize']:
#     if confdata['type']['file']:
#         from deserialize_json import DeserializeJson
#         newDeserializator = DeserializeJson(confdata['paths']['source_folder'] + confdata['paths']['json_source_file'])
#         newDeserializator.somestats()
#
#     if confdata['operations']['serialize']:
#         if confdata['type']['file']:
#             from serialize_json import SerializeJson
#             SerializeJson.run(newDeserializator, confdata['paths']['source_folder'] + confdata['paths']['json_destination_file'])
#
# if confdata['operations']['convert_to_yaml']:
#     if confdata['type']['file']:
#         from convert_json_to_yaml import ConvertJsonToYaml
#         ConvertJsonToYaml.run(confdata['paths']['source_folder'] + confdata['paths']['json_source_file'],
#                               confdata['paths']['source_folder'] + confdata['paths']['yaml_destination_file'])

# Z pliku JSON na plik XML
from deserialize_json import DeserializeJson
newDeserializator = DeserializeJson('Assets/data.json')
newDeserializator.somestats()

from serialize_xml import SerializeXML
SerializeXML.run(newDeserializator, 'Assets/data_xmod.xml')

# Z pliku XML na plik JSON
from deserialize_xml import DeserializeXML
deserialized_data = DeserializeXML.run("Assets/data.xml")

from serialize_json import SerializeJson
SerializeJson.run_xml(deserialized_data, "Assets/data_xmod.json")
