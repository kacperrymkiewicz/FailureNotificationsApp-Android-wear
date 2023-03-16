import { PriorityType, StatusType } from '../../../typings/awaria.types'

export class CreateAwariaDto {
  opis_awarii: string;
  status: StatusType
  priorytet: PriorityType
  stanowisko: number;
}
