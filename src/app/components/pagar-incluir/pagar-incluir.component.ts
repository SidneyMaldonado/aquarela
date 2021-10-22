import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Pagar } from 'src/app/entities/pagar';
import { PagarService } from 'src/app/services/pagar.service';

@Component({
  selector: 'app-pagar-incluir',
  templateUrl: './pagar-incluir.component.html',
  styleUrls: ['./pagar-incluir.component.css']
})
export class PagarIncluirComponent implements OnInit {

  form: FormGroup = this.formBuilder.group({
    idPagar: ['', Validators.required],
    idTipoPagar: ['', Validators.required],
    idFornecedor: ['', Validators.required],
    nmPagar: ['', Validators.required],
    qtdParcela: ['', Validators.required],
    dtEmissao: ['', Validators.required],
    nrValor: ['', Validators.required],
    dmSituacao: ['', Validators.required],
    dtPrimeiroVencimento: ['', Validators.required],
  })

  constructor(private pagarService: PagarService,
     private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  submit(){
    if(this.form.valid){
      const novoPagar: Pagar = {
      idPagar: 0,
      idTipoPagar: this.form.value.idTipoPagar,
      idFornecedor: this.form.value.idFornecedor,
      nmPagar: this.form.value.nmPagar,
      qtdParcela: this.form.value.qtdParcela,
      dtEmissao: this.form.value.dtEmissao,
      nrValor: this.form.value.nrValor,
      dmSituacao: Boolean(this.form.value.dm),
      dtPrimeiroVencimento: this.form.value.dtPrimeiroVencimento
      //const novoPagar: Pagar = this.form.value
      //novoPagar.dmSituacao=Boolean(novoPagar.dmSituacao)
    }
    console.log(novoPagar)
    this.pagarService.incluir(novoPagar).subscribe(res => console.log(res), error => console.log(error))
  }else{
    alert('Dados inválidos!')
  }

}
}
