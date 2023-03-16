import { HttpException, HttpStatus, Injectable } from '@nestjs/common';
import { dataSource } from 'src/core/database/database.provider';
import { Awaria, Stanowisko } from 'src/core/database/entities';
import { CreateAwariaDto } from '../dtos/create-awaria.dto';
import { Gateway } from '../../../gateway/gateway';

@Injectable()
export class AwariaService {
  constructor(private gateway: Gateway) {}
  async awariaList() {
    const awarie = await dataSource.getRepository(Awaria).find();
    return awarie;
  }

  async awariaById(id) {
    const awaria = await dataSource
      .createQueryBuilder()
      .select('awaria')
      .from(Awaria, 'awaria')
      .where('awaria.id = :_id', { _id: id })
      .getOne();
    if (awaria) return awaria;
    throw new HttpException(
      'Nie znaleziono awarii o podanym ID',
      HttpStatus.NOT_FOUND,
    );
  }

  async createAwaria(createAwariaDto: CreateAwariaDto) {
    const newAwaria = new Awaria();

    const stanowisko = await dataSource
      .getRepository(Stanowisko)
      .findOne({ where: { id: createAwariaDto.stanowisko } });

    if (!stanowisko)
      throw new HttpException(
        'Nie znaleziono stanowiska o podanym ID',
        HttpStatus.NOT_FOUND,
      );

    newAwaria.opis_awarii = createAwariaDto.opis_awarii;
    newAwaria.priorytet = createAwariaDto.priorytet;
    newAwaria.stanowisko = stanowisko;
    newAwaria.status = createAwariaDto.status;

    const awariaRepository = await dataSource.getRepository(Awaria);
    await awariaRepository.save(newAwaria);

    this.gateway.server.emit('newAwaria', { newAwaria });
    console.log('Nowa awaria');

    return 'Success';
  }

  async claimAwaria(id) {
    const awariaRepository = await dataSource.getRepository(Awaria);
    try { 
      await awariaRepository.update(id, {status: 2});
    } 
    catch(e) {
      throw new HttpException(
        `Nie znaleziono awarii o podanym id równym < ${id} >`,
        HttpStatus.NO_CONTENT,
      );
    }
  }

  async finishAwaria(id) {
    const awariaRepository = await dataSource.getRepository(Awaria);
    try { 
      await awariaRepository.update(id, {status: 3});
    } 
    catch(e) {
      throw new HttpException(
        `Nie znaleziono awarii o podanym id równym < ${id} >`,
        HttpStatus.NO_CONTENT,
      );
    }
  }
}
