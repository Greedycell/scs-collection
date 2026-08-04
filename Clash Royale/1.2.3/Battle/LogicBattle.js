class LogicBattle {
  constructor(duration = 380) {
    this.duration = duration
    this.elapsed = 0
  }

  get battleTime() {
    const remaining = this.duration - this.elapsed
    return remaining > 0 ? remaining * 2 : 0
  }

  get battleSeconds() {
    const sec = Math.floor(this.battleTime / 4)
    console.log('Battle Seconds: ' + sec)
    return sec
  }

  Tick(seconds) {
    this.elapsed += seconds
    if (this.elapsed > this.duration) this.elapsed = this.duration
    return this.battleSeconds
  }
}

module.exports = LogicBattle