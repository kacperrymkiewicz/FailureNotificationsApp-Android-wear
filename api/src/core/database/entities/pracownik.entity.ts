import { Entity, PrimaryGeneratedColumn, Column, CreateDateColumn } from "typeorm"

@Entity()
export class Pracownik {
    @PrimaryGeneratedColumn({
        type: "int",
    })
    id: number;

    @Column({
        type: "varchar",
        nullable: false,
    })
    imie: string;

    @Column({
        type: "varchar",
        nullable: false,
    })
    nazwisko: string;

    @Column({
        type: "varchar",
        nullable: false,
    })
    login: string;

    @Column({
        type: "varchar",
        length: 256,
        nullable: false,
    })
    haslo: string;

}
