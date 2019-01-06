# Pac-Man

## Autores

* Flávio Santos - 21702344

* Leandro Brás - 21801026

## Repositório Remoto Usado

### [Pac-Man](https://github.com/xShadoWalkeR/PacMan)

## Repartição do Projeto

É de salientar que o objetivo neste projeto foi sempre tentar dividir o trabalho pelos dois o mais possível.
Partes do código foram feitas em conjunto (incluindo *debugging*), embora apenas uma pessoa tenha dado os *commits* necessários.

#### Flávio
* Criou o Menu
* Criou a randomização do movimento dos fantasmas
* Implementou os estados dos fantasmas (incluindo criação das várias ações que podem desempenhar) [REPARTIDO]
* Retrabalhou o *GameLoop*
* Diagrama UML

#### Leandro
* Criou a maneira de ler os ficheiros de texto e colocar os seus conteúdos no nível
* Retrabalhou os estados dos fantasmas (incluindo criação das várias ações que podem desempenhar) [REPARTIDO]
* Criou o *GameLoop*
* Implementou o movimento dos personagens
* Fluxograma

Este Relatório foi feito por ambos.

## Descrição da Solução

### Explicação

O projeto é composto por 9 classes e 2 enumerações.

A classe `Program` faz com que a janela da Consola tenha dimensões pré-definidas e não mostre o cursor da própria. É ainda criada uma instância da classe `Menu`.

A classe `Menu` é responsável pela "construção" de um Menu Principal para o jogo. Para tal efeito, possui duas instâncias no seu construtor: uma do tipo `KeyReader` e outra do tipo `Sprite`.
A primeira está ligada ao *input* que a consola recebe por parte do utilizador; A segunda é necessária pois contém em si os desenhos que serão necessários mostrar (com cores diferentes).
Dentro desta classe existem então 3 métodos diferentes que fazem tudo isto.
O primeiro, `GetInput()`, vai buscar a `string` `Input` da instância da classe `KeyReader` e, de acordo com o valor da variável `playSelected`, seleciona *PLAY* ou *QUIT*. Existe ainda *input* para a tecla *Enter* que, de acordo com a mesma variável anterior, inicia um novo jogo (ao criar uma instância da classe `Game`) se esta se encontrar a `true` ou fecha a Consola se esta se encontrar a `false`. O segundo método (`LoadMenu()`) é responsável por ler o ficheiro de texto com as bordas do nível (apenas para efeitos estéticos). Já o terceiro método (`RenderMenu()`), como o nome indica, desenha tudo na Consola, chamando no final o método `GetInput`.

A classe `KeyReader` é a classe que se responsabiliza por qualquer input por parte do utilizador. Existem em si, 2 métodos: `GetInput()`, que lê a tecla pressionada e a adiciona a uma coleção (`BlockingCollection<T>`) dentro de um *loop* `do-while`; e `ReadFromQueue()`, que deteta a tecla pressionada e a verifica dentro de um `while`, alterando a variável global `Input` conforme.
Ambos este métodos correm em `Threads` separadas, sendo estas iniciadas (com um `Start()`) no construtor desta classe. Nesta classe existe a utilização do *Observer pattern*, de modo a chamar as teclas que foram pressionadas.

A classe `Sprite` apenas contém todas as *sprites* usadas pelo Menu e pelo jogo em formato de variáveis globais.

A classe `Game` possui 7 variáveis globais, sendo 6 delas instanciadas no seu construtor. Estas instâncias são tudo o que o jogo precisa para correr: o *PacMan*, os fantasmas e o próprio nível. No final do construtor existe ainda a chamada ao método `GameLoop()`, que como o nome indica funciona de *loop* ao jogo. Este método contém um `while` que desenha, atualiza (através de `Updates`) e apaga os pontos "comestíveis", e o *PacMan* e os fantasmas anteriormente instanciados. A distância temporal entre o desenhar e apagar é de `25` milisegundos. Existe ainda uma condição que dita que se o *PacMan* ganhar (verificado através do valor retornado do método `WinCondition()` na classe `PacMan`) os pontos são repostos pelo nível e o *PacMan* e os fantasmas fazem *respawn*. Quando a vida do *PacMan* chega a `0`, sai-se do `while`, limpa-se a consola e retorna-se ao Menu. Existe ainda nesta classe o método `ReadInput()` que lê as teclas pressionadas pelo utilizador e altera a direção do *PacMan* de acordo com o valor de `Input`. Este método é chamado dentro do *GameLoop*.

A classe `Level` é responsável pelo desenho do nível e respetivos *colliders* que este possua. Para tal, possui uma instância da classe `LevelLoader` e `Sprite` de forma a poder saber o que está a desenhar (pontos, paredes, *score*, etc.), e `4` *arrays*, sendo `2` deles multidimensionais. Existem 6 métodos que não o construtor. O método `GetCollider()` obtém os *colliders* das paredes e dos pontos percorrendo os *arrays* respetivos; `RenderUi()` desenha a *score* do *PacMan*, atribuindo cada número que esta possa ser a uma *sprite* pré-definida para cada um (é lida através de um *array* de `strings`); `CheckNumber()` é o método chamado para atribuir as vidas e o nível a cada *sprite* correspondente (neste caso, já não é necessário ser com `array`, daí haverem o `atual` e o anterior); `RenderPoints()` e `RenderLevel()`, como o nome indica, desenham na Consola os pontos comestíveis, o logo do *PacMan* e as paredes pertencentes ao nível atribuindo uma cor específica a cada um. Existe ainda o `Update()`, que chama o `RenderUi()` constantemente, chamando o `RenderPoints()` apenas de `10` em `10` *frames*. O Construtor desta classe chama o `GetCollider()` e o `RenderLevel()`, pois estes só são necessários quando chamados para instanciação.

A classe `LevelLoader` tem como função ir buscar o nível e os pontos comestíveis aos ficheiros de texto. Possui o nome destes como *strings*, passando os seus conteúdos para os *arrays* que lhes correspondem através do uso da classe `StreamReader` e do método `ReadLine()` da mesma, dentro do método `LoadLevel()` Este método é chamado `2` vezes no construtor da classe, uma para cada ficheiro `txt`.

A classe `PacMan` tal como o nome indica é a classe responsável por tudo o que o *PacMan* faz. Este possui um `X` e um `Y`, um dicionário com as animações, *sprites* específicas, temporizador para as animações, controlador de velocidade de movimento, uma direção, um timer, vida, nível e pontos. Possui ainda `2` eventos de forma a dizer aos fantasmas que está morto e que comeu um ponto especial (os maiores, amarelos). No seu construtor, todas as variáveis globais são inicializadas com os valores respetivos (como por exemplo, vida a `3` ou nível a `1`). Estão presentes nesta classe `9` métodos: `Plot()` e `Unplot()` são responsáveis pelo desenho do *PacMan*; `FixDirection()` verifica se o *PacMan* pode efetivamente mover-se na zona desejada pelo utilizador; `CheckPointsCollision()` verifica se o *PacMan* "comeu" os pontos (se tiver comido os pontos especiais, um evento é lançado e os fantasmas tornam-se vulneráveis e comestíveis também), incrementando `10` pontos de *score* a cada colisão, e decrementando `1` a `totalPoints` de forma a que o *PacMan* se aperceba de quantos pontos ainda existem no nível atual; `Move()` muda a animação do *PacMan* conforme a sua direção e reduz ou aumenta o seu `X` ou `Y` (se embater numa parede, não se move); `CheckToroidal()` faz com que o *PacMan* se teletransporte para o lado contrário do nível quando não existe parede, mas este saiu do limite do mundo; `Respawn()` faz com que o *PacMan* faça *respawn* (dentro deste método existe uma condição que pergunta se ele morreu ou não - dependendo do valor da variável este pode ou não perder vida e lançar um evento aos fantasmas a informá-los que morreu); `WinCondition()` retorna um valor `true` ou `false` dependendo dos pontos atuais presentes no nível (ele verifica isto, através da variável `totalPoints` como já foi referido) - no caso de ser `true` ele altera algumas variáveis (vida, nível, *score*, etc.). Tal como na classe `Level`, existe ainda o método `Update()`, chama os métodos `CheckPointsCollision()` e `Move()`. O `Plot()`, `Unplot()` e `Update()` são todos chamados individualmente no *Game Loop*.

A classe `Ghost` tal como o nome indica é a classe responsável por tudo o que cada fantasma faz. Existem algumas variáveis que possuem exatamente o mesmo tipo de função do que no *PacMan* (como o `x` e o `y`), mas existem outras que são exclusivas deste como `isVulnerable` (se estiver a `true`, pode ser comido), `state` (que declara o estado atual em que se encontra), ou a propriedade `IsDead` (que declara se foi comido pelo *PacMan*). O construtor de `Ghost` assegura uma instância para quase todas as variáveis globais. Existem ainda uns métodos em comum, embora tenham as suas respetivas diferenças: `Plot()` e `Unplot()` que, essencialmente apenas desenham e apagam os fantasmas; `Move()`, que direcionam os aumentam ou diminuem o seu `x` ou `y` de acordo com a direção, fazendo com que vão na direção oposta quando embatem numa parede; `CheckToroidal()` que funciona exatamente da mesma maneira do que na classe `PacMan`. De entre os métodos exclusivos dos fantasmas estão o `UpdateDirection()` (que muda a direção dos fantasmas, de acordo com o número `chance` que se obtiver, se este se encontrar num sítio com vários caminhos - isto não anula o facto de poder ainda voltar para trás), `UpdateState()` (que serve de referência ao que cada estado dos fantasmas pode ser, ou seja, cada estado dita o que cada fantasma pode ou está a fazer), `ReturnToSpawn()` (que tem uma condição que depende do facto do fantasma em causa estar morto, ditando diferentes comportamentos - o fantasma também pode voltar ao *spawn* se o jogo der *reset*, por exemplo), `LeaveSpawn()` (que faz com que cada fantasma possa sair do *spawn*, e de acordo com o número tem ou não de esperar que outros saiam primeiro), `CheckPacMan()` (que verifica se os fantasmas conseguem ou não ver o *PacMan*, retornando `true` se sim e `false` se não), `Follow()` (que faz com que os fantasmas sigam o *PacMan* se o virem e lhes atribui uma direção dependendo do lado em que ele foi visto), `Run()` (que é o contrário do método anterior, fazendo com que os fantasmas sigam na direção contrária a de onde vêem o *PacMan*, reduzindo as suas velocidades, alterando as suas cores e tornando-os vulneráveis), `BackToNormal()` (que faz com que os fantasmas retornem ao normal, deixando-os não vulneráveis, alterem a cor para a original e normalizem a velocidade), `CheckCollision()` (que verifica se os fantasmas colidiram com o *PacMan* ao verificar se as extremidades do fantasma em causa intersetam as do *PacMan* - se estiverem vulneráveis, morrem, se não quem morre é o *PacMan* e este é forçado a dar *respawn*) e finalmente `Reboot()` (que serve de método a ser chamado quando o nível dá *reset* - ou seja, quando o *PacMan* "ganha"). Existe ainda um `Update()` no qual os fantasmas estão constantemente a verificar a colisão com o *PacMan* e o estado destes é alterado (este método é chamado no *GameLoop*).

A enumeração `Direction` dita as `5` direções nas quais tanto o *PacMan* como os fantasmas se podem mover (*Esquerda*, *Direita*, *Cima*, *Baixo* ou *Nenhuma*).

A enumeração `GhostState` dita os `5` estados nos quais os fantasmas se podem encontrar (*SairDoSpawn*, *ProcurarPacMan*, *CorrerAtrásDoPacMan*, *FugirDoPacMan* ou *VoltarAoSpawn*).

### Diagrama UML
![Diagrama_UML](PacMan_UML.png)

### Fluxograma
![Fluxograma](PacMan_Fluxograma.png)

## Conclusões e Matéria Aprendida

A estruturação de classes é algo extremamente importante, muito realçado pela criação e desenvolvimento deste projeto. A divisão de tarefas pelas mesmas tem de ser algo que faça sentido, algo para o qual qualquer programador poderia olhar e imediatamente entender.

Algo também muito importante neste projeto foi o uso de `Threads` (algo que ajudou no desenvolvimento do projeto, pois ajuda a pedir *input* do jogador sem afetar o desempenho da *thread* principal) e a compreensão de como um *GameLoop* é executado.

Outra coisa que serviu de bastante ajuda foi o uso (e desuso) de alguns *patterns* que garantem estabilidade ao código e ofereceram algumas vantagens que nos ajudaram na resolução de alguns problemas.

## Referências

Whitaker, RB. (2016). *C# Player's Guide*, Starbound Software

[.NET API Browser](https://docs.microsoft.com/en-us/dotnet/api/)
