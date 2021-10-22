import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ParcelaPagar } from 'src/app/entities/parcelapagar';
import { showError, showSuccess } from 'src/app/formHandler/formhandler';
import { CarteiraService } from 'src/app/services/carteira.service';
import { ParcelaPagarService } from 'src/app/services/parcelapagar.service';

@Component({
  selector: 'app-parcelapagar-incluir',
  templateUrl: './parcelapagar-incluir.component.html',
  styleUrls: ['./parcelapagar-incluir.component.css']
})
export class ParcelapagarIncluirComponent implements OnInit {

  form: FormGroup = this.formBuilder.group({
    
    nrParcela: ['', Validators.required],
    dsDocumento: ['', Validators.required],
    dtVencimento: ['', Validators.required],
    dmSituacao: ['', Validators.required],
    nrValor: ['', Validators.required],
    dtPagamento: ['', Validators.required],
    nrValorPago: ['', Validators.required],
    idPagar: ['', Validators.required],
    idContaCaixa: ['', Validators.required],
    
  })
  constructor(private parcelaPagarService: ParcelaPagarService,
    private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  submit() {
    if(this.form.valid) {
      const novaParcelaPagar: ParcelaPagar = {
        idParcela: 0,
        nrParcela: this.form.value.nrParcela,
        dsDocumento: this.form.value.dsDocumento,
        dtVencimento: this.form.value.dtVencimento,
        dmSituacao: Boolean(this.form.value.dmSituacao),
        nrValor: this.form.value.nrValor,
        dtPagamento: this.form.value.dtPagamento,
        nrValorPago: this.form.value.nrValorPago,
        idPagar: this.form.value.idPagar,
        idContaCaixa: this.form.value.idContaCaixa
      }
      console.log(novaParcelaPagar)
      this.parcelaPagarService.incluir(novaParcelaPagar).subscribe(res => {
        showSuccess("Carteira inserida")
      }, error => {
        console.log(error)
        if(error.status == 200) {
          showSuccess("Carteira inserida")
          setTimeout(() => {
            window.location.reload()
          }, 2000)
        }
      })
    } else {
      showError("Dados inválidos")
    }
  }

}
