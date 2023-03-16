import { Test, TestingModule } from '@nestjs/testing';
import { AwariaService } from './awaria.service';

describe('AwariaService', () => {
  let service: AwariaService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [AwariaService],
    }).compile();

    service = module.get<AwariaService>(AwariaService);
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });
});
