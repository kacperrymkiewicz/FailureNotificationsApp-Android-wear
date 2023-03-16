import * as dotenv from 'dotenv';
import { Awaria, Pracownik, Raport, Stanowisko } from '../entities';
import { SqlServerConnectionOptions } from 'typeorm/driver/sqlserver/SqlServerConnectionOptions';

dotenv.config();

export const DB_Config: SqlServerConnectionOptions = {
  type: 'mssql',
  host: process.env.HOST,
  port: 1433,
  database: process.env.DBNAME,
  username: process.env.DBUSERNAME,
  password: process.env.DBPASS,
  entities: [Awaria, Pracownik, Raport, Stanowisko],
  synchronize: true,
};
