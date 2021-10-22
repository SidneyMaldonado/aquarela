import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SaldoAtualizadoLista } from 'src/app/entities/saldoAtualizadoLista';
import { showError, showSuccess } from 'src/app/formHandler/formhandler';
import { SaldoatualizadoService } from 'src/app/services/saldoatualizado.service';

@Component({
  selector: 'app-saldoatualizado-incluir',
  templateUrl: './saldoatualizado-incluir.component.html',
  styleUrls: ['./saldoatualizado-incluir.component.css']
})
export class SaldoatualizadoIncluirComponent implements OnInit {

  form: FormGroup = this.formBuilder.group({
    dtSaldoAtualizado: ['', Validators.required],
    vlCaixa: ['', Validators.required],
    vlDeposito: ['', Validators.required],
    vlPagseguro: ['', Validators.required],
    vlItau: ['', Validators.required],
    vlBb: ['', Validators.required],
    situacao: ['', Validators.required],
  })

  constructor(private saldoServico: SaldoatualizadoService,  private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  submit() {
    if(this.form.valid) {
      const novoSaldo: SaldoAtualizadoLista = {
        idSaldoAtualizado: 0,
        dtSaldoAtualizado: this.form.value.dtSaldoAtualizado,
        vlCaixa: this.form.value.vlCaixa,
        vlDeposito: this.form.value.vlDeposito,
        vlPagseguro: this.form.value.vlPagseguro,
        vlItau: this.form.value.vlItau,
        dmSituacao: Boolean(this.form.value.situacao),
        vlBb: this.form.value.vlBb
      }
      console.log(novoSaldo)
      this.saldoServico.incluir(novoSaldo).subscribe(res => {
        
        showSuccess("Saldo inserido")
      }, error => {
        if(error.status == 200) {console.log(error);showSuccess('Saldo inserido')}
        else console.log(error)
      })
    } else {
      showError("Dados inválidos!")
    }
  }

}
