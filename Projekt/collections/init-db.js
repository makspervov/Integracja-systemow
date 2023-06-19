db = db.getSiblingDB('CO2');

const collections = [
  { name: 'co2emissionbycountries', file: '/docker-entrypoint-initdb.d/co2emissionbycountries.json' },
  { name: 'co2emissionbysectors', file: '/docker-entrypoint-initdb.d/co2emissionbysectors.json' },
  { name: 'forestareabycountries', file: '/docker-entrypoint-initdb.d/forestareabycountries.json' },
  { name: 'landareas', file: '/docker-entrypoint-initdb.d/landareas.json' },
  { name: 'users', file: '/docker-entrypoint-initdb.d/users.json' }
];

collections.forEach(collection => {
  db.createCollection(collection.name);
  const cmd = `mongoimport --db CO2 --collection ${collection.name} --type json ${collection.file} --jsonArray`;
  const result = runShellCommand(cmd);
  print(result);
});

function runShellCommand(cmd) {
  const exec = require('child_process').execSync;
  try {
    return exec(cmd).toString();
  } catch (error) {
    return error.toString();
  }
}
