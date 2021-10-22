import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Carteira } from 'src/app/entities/carteira';
import { CarteiraService } from 'src/app/services/carteira.service';
import { showError,showSuccess } from 'src/app/formHandler/formhandler';

@Component({
  selector: 'app-carteira-incluir',
  templateUrl: './carteira-incluir.component.html',
  styleUrls: ['./carteira-incluir.component.css']
})
export class CarteiraIncluirComponent implements OnInit {


  form: FormGroup = this.formBuilder.group({
    
    nmHistorico: ['', Validators.required],
    nrCredito: ['', Validators.required],
    nrDebito: ['', Validators.required],
    dmSituacao: ['', Validators.required],
    nrSaldo: ['', Validators.required],
    idCliente: ['', Validators.required],
    
  })
  constructor(private carteiraService: CarteiraService,
    private formBuilder: FormBuilder,) { }

  ngOnInit(): void {
  }

  submit() {
    if(this.form.valid) {
      const novaCarteira: Carteira = {
        idCarteira: 0,
        dtInclusao: new Date,
        nmHistorico: this.form.value.nmHistorico,
        nrCredito: this.form.value.nrCredito,
        nrDebito: this.form.value.nrDebito,
        dmSituacao: Boolean(this.form.value.dmSituacao),
        nrSaldo: this.form.value.nrSaldo,
        idCliente: this.form.value.idCliente,
      }
      console.log(novaCarteira)
      this.carteiraService.incluir(novaCarteira).subscribe(res => {
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
