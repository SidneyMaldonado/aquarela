import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Fornecedor } from 'src/app/entities/fornecedor';
import { FornecedorService } from 'src/app/services/fornecedor.service';

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
    nmEmail: ['', Validators.required],
    logo: ['', Validators.required],
    obs: ['', Validators.required],
    situacao: ['', Validators.required],
  })
  
  
  constructor(private fornecedorService: FornecedorService,
    private formBuilder: FormBuilder,
    ) { }

  ngOnInit(): void {
  }

  showError(msg: string) {
    const notificacao = document.getElementById("notificacao")
    if(notificacao) {
      notificacao.innerHTML = msg
      notificacao.classList.add("err")
      notificacao.style.opacity = "1"
      setTimeout(() => notificacao.style.opacity = "0", 2000)
    }
  }

  showSuccess(msg: string) {
    const notificacao = document.getElementById("notificacao")
    if(notificacao) {
      notificacao.innerHTML = msg
      notificacao.classList.add("success")
      notificacao.style.opacity = "1"
      setTimeout(() => notificacao.style.opacity = "0", 2000)
    }
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
        this.showSuccess("Fornecedor inserido")
      }, error => {
        if(error.status == 200) this.showSuccess('Fornecedor inserido')
        else console.log(error)
      })
    } else {
      this.showError("Dados inválidos!")
    }
  }

}
