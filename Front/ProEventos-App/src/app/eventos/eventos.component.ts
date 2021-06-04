import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  private _listFilter: string = '';

  public eventos: any = [];
  public eventosFiltrados: any = [];
  public widthImg: number = 100;
  public marginImg: number = 2;
  public showImg: boolean = true;


  public get listFilter(): string
  {
    return this._listFilter;
  }

  public set listFilter(value: string)
  {
    this._listFilter = value;
    this.eventosFiltrados = this.listFilter ? this.eventFilter(this.listFilter) : this.eventos;
  }

  public eventFilter(searchBy: string): any
  {
    searchBy = searchBy.toLowerCase();
    return this.eventos.filter((evento: { tema: string; localName: string }) =>
      evento.tema.toLocaleLowerCase().indexOf(searchBy) !== -1 ||
      evento.localName.toLocaleLowerCase().indexOf(searchBy) !== -1
    );
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void
  {
    this.getEventos();
  }

  btnShowImgChange()
  {
    this.showImg = !this.showImg;
  }



  public getEventos():void
  {
    this.http.get('https://localhost:5001/api/eventos').subscribe(
      response => {
        this.eventos = response;
        this.eventosFiltrados = this.eventos;
      },
      error => console.log(error));
  }

}
