import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Fornecedor } from 'src/app/entities/fornecedor';
import { FornecedorService } from 'src/app/services/fornecedor.service';
import { showError, showSuccess } from 'src/app/formHandler/formhandler';

@Component({
  selector: 'app-fornecedor-incluir',
  templateUrl: './fornecedor-incluir.component.html',
  styleUrls: ['./fornecedor-incluir.component.css']
})
export class FornecedorIncluirComponent implements OnInit {

  form: FormGroup = this.formBuilder.group({
    nome: ['', Validators.required],
    nmContato: ['', Validators.required],
    nrTelefone: ['', Validators.required],
    nmEmail: ['', [Validators.required, Validators.email, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]],
    logo: ['', Validators.required],
    obs: ['', Validators.required],
    situacao: ['', Validators.required],
  })
  
  
  constructor(private fornecedorService: FornecedorService,
    private formBuilder: FormBuilder,
    ) { }

  ngOnInit(): void {
  }

  submit() {
    if(this.form.valid) {
      const novoFornecedor: Fornecedor = {
        idFornecedor: 0,
        nmFornecedor: this.form.value.nome,
        nmContato: this.form.value.nmContato,
        nrTelefone: this.form.value.nrTelefone,
        nmEmail: this.form.value.nmEmail,
        urlLogotipo: this.form.value.logo,
        dsObservacao: this.form.value.obs,
        dmSituacao: Boolean(this.form.value.situacao)
      }
      this.fornecedorService.incluir(novoFornecedor).subscribe(res => {
        console.log(res)
        showSuccess("Fornecedor inserido")
      }, error => {
        if(error.status == 200) showSuccess('Fornecedor inserido')
        else console.log(error)
      })
    } else {
      showError("Dados inválidos!")
    }
  }

}
