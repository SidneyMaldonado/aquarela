import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GrupoProduto } from 'src/app/entities/grupoproduto';
import { showSuccess, showError } from 'src/app/formHandler/formhandler';
import { GrupoProdutoService } from 'src/app/services/grupoproduto.service';

@Component({
  selector: 'app-grupoproduto-incluir',
  templateUrl: './grupoproduto-incluir.component.html',
  styleUrls: ['./grupoproduto-incluir.component.css']
})
export class GrupoprodutoIncluirComponent implements OnInit {

  form: FormGroup = this.formBuilder.group({
    dsImagem: ['', Validators.required],
    dsNomeGrupo: ['', Validators.required],
    dsDescricao: ['', Validators.required],
    situacao: ['', Validators.required],
  })

  constructor(private grupoProdutoService: GrupoProdutoService, private formBuilder: FormBuilder) { }

  ngOnInit(): void {
  }

  submit() {
    if(this.form.valid) {
      const novoGrupo: GrupoProduto = {
        idGrupoProduto: 0,
        dsImagem: this.form.value.dsImagem,
        dsNomeGrupo: this.form.value.dsNomeGrupo,
        dsDescricao: this.form.value.dsDescricao,
        dmSituacao: Boolean(this.form.value.situacao)
      }
      this.grupoProdutoService.incluir(novoGrupo).subscribe(res => {
        console.log(res)
        showSuccess("Grupo inserido")
      }, error => {
        if(error.status == 200) {
          showSuccess('Grupo inserido')
          setTimeout(() => window.location.reload(), 2000)
        }
        else console.log(error)
      })
    } else {
      showError("Dados inválidos!")
    }
  }

}
