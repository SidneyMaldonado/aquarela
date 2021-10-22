import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ContaCaixa } from 'src/app/entities/contacaixa';
import { showError, showSuccess } from 'src/app/formHandler/formhandler';
import { ContaCaixaService } from 'src/app/services/contacaixa.service';

@Component({
  selector: 'app-conta-caixa-inserir',
  templateUrl: './conta-caixa-inserir.component.html',
  styleUrls: ['./conta-caixa-inserir.component.css']
})
export class ContaCaixaInserirComponent implements OnInit {
  form:FormGroup = this.formBuilder.group({
    dmSituacao:['', Validators.required],
    nmContaCaixa:['', Validators.required],
    vlSaldoInicial:['', Validators.required],
    idCarteira:['', Validators.required],
    idSaldoAtualizado:['', Validators.required]
  })
  constructor(private contaCaixaService: ContaCaixaService, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  incluir(){
    if(!this.form.valid){
      showError("Dados inválidos");
      return;
    }
    let bodyContaCaixa:ContaCaixa= this.form.value
    bodyContaCaixa.dmSituacao = Boolean(bodyContaCaixa.dmSituacao)
    this.contaCaixaService.incluir(bodyContaCaixa).subscribe(
      response => alert(response),
      error =>{if(error.status==200){showSuccess("Sucesso ao incluir!")}}
    )
  }

}
