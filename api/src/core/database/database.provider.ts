import { DB_Config } from './config/database.config.js';
import { DataSource } from 'typeorm';

const dataSource = new DataSource(DB_Config);

export { dataSource };
