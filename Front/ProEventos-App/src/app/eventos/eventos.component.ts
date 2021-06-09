import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit
{
  modalRef!: BsModalRef;

  private filtroListado = '';

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];
  public widthImg = 100;
  public marginImg = 2;
  public showImg = true;


  public get listFilter(): string
  {
    return this.filtroListado;
  }

  public set listFilter(value: string)
  {
    this.filtroListado = value;
    this.eventosFiltrados = this.listFilter ? this.eventFilter(this.listFilter) : this.eventos;
  }

  public eventFilter(searchBy: string): Evento[]
  {
    searchBy = searchBy.toLowerCase();
    return this.eventos.filter((evento: { tema: string; localName: string }) =>
      evento.tema.toLocaleLowerCase().indexOf(searchBy) !== -1 ||
      evento.localName.toLocaleLowerCase().indexOf(searchBy) !== -1
    );
  }

  constructor(
    private eventoService: EventoService,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
    ) { }

  public ngOnInit(): void
  {
    this.spinner.show();
    this.getEventos();
  }

  public btnShowImgChange(): void
  {
    this.showImg = !this.showImg;
  }

  public getEventos(): void
  {
    this.eventoService.getEventos().subscribe(
      {
        next: (eventos: Evento[]) => {
        this.eventos = eventos;
        this.eventosFiltrados = this.eventos;
      },
      error: () => {
        this.spinner.hide();
        this.toastr.error('Erro ao carregar os eventos', 'Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }

  openModal(template: TemplateRef<unknown>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.modalRef.hide();
    this.toastr.success('O Evento foi deletado com sucesso', 'Deletado!');
  }

  decline(): void {
    this.modalRef.hide();
  }
}
